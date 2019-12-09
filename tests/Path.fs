module Tests.Path

open System
open Fable.Core
open Fable.Core.JsInterop
open Node.Path
open Util

let path = Node.Api.path
let testAbsolute p result = path.isAbsolute p |> equal result

let tests : Test = 
  testList "Path" [
  
    testList "path.basename" [
      testCase "Windows" <| fun _ ->
        if isPosix then testPassed()
        else
          path.basename "c:\\windows\\command.com"
            |> equal "command.com"

      testCase "win32" <| fun _ ->
        if isPosix then testPassed()
        else
          path.win32.basename "c:\\windows\\command.com"
            |> equal "command.com"        

      testCase "POSIX" <| fun _ ->
        if not isPosix then testPassed()
        else
          path.basename "c:\\windows\\command.com" 
            |> equal "c:\\windows\\command.com"

      testCase "POSIX" <| fun _ ->
        if not isPosix then testPassed()
        else
          path.posix.basename "/tmp/index.html"
            |> equal "index.html"                
    ]

    testList "path.delimiter" [
      testCase "Windows" <| fun _ -> 
        if isPosix then testPassed()
        else
          path.delimiter |> equal ";"

      testCase "POSIX" <| fun _ -> 
        if not isPosix then testPassed()
        else
          path.delimiter |> equal ":"
    ]

    testList "path.dirname" [
      testCase "returns directory name" <| fun _ -> path.dirname "/foo/bar/baz/asdf/quux" |> equal "/foo/bar/baz/asdf"
    ]

    testList "path.extname" [ 
      testCase "returns the extension of the path" <| fun _ -> path.extname "index.html" |> equal ".html"
      testCase "returns the extension of the path" <| fun _ -> path.extname "index.coffee.md" |> equal ".md"
      testCase "returns the extension of the path" <| fun _ -> path.extname "index." |> equal "."
      testCase "returns the extension of the path" <| fun _ -> path.extname "index" |> equal ""
      testCase "returns the extension of the path" <| fun _ -> path.extname ".index" |> equal ""
    ]

    testList "path.format" [
      testCase "Windows" <| fun _ -> 
        if isPosix then testPassed()
        else
          let options = jsOptions<Node.Path.PathObjectProps>(fun o -> 
            o.dir <- "C:\\path\\dir"
            o.baseName <- "file.txt" // `base` in javascript
          )
          path.format options |> equal "C:\\path\\dir\\file.txt"

      testCase "POSIX" <| fun _ -> 
        if not isPosix then testPassed()
        else
          let options = jsOptions<Node.Path.PathObjectProps>(fun o -> 
            o.root <- "ignored"
            o.dir <- "/home/user/dir"
            o.baseName <- "file.txt" // `base` in javascript
          )
          path.format options |> equal "/home/user/dir/file.txt"
    ]        

    testList "path.absolute" [        
      testCase "Windows" <| fun _ -> if isPosix then testPassed() else testAbsolute "//server" true
      testCase "Windows" <| fun _ -> if isPosix then testPassed() else testAbsolute "\\\\server" true
      testCase "Windows" <| fun _ -> if isPosix then testPassed() else testAbsolute "C:/foo/.." true
      testCase "Windows" <| fun _ -> if isPosix then testPassed() else testAbsolute "C:\\foo\\.." true
      testCase "Windows" <| fun _ -> if isPosix then testPassed() else testAbsolute "bar\\baz" false
      testCase "Windows" <| fun _ -> if isPosix then testPassed() else testAbsolute "bar/baz" false
      testCase "Windows" <| fun _ -> if isPosix then testPassed() else testAbsolute "." false
      testCase "POSIX" <| fun _ -> if not isPosix then testPassed() else testAbsolute "/foo/bar" true
      testCase "POSIX" <| fun _ -> if not isPosix then testPassed() else testAbsolute "/baz/.." true
      testCase "POSIX" <| fun _ -> if not isPosix then testPassed() else testAbsolute "qux/.." false
      testCase "POSIX" <| fun _ -> if not isPosix then testPassed() else testAbsolute "." false
    ]
    
    testList "path.join" [
      testCase "windows" <| fun _ -> if isPosix then testPassed() else path.join [|"/foo";"bar";"baz/asdf";"quux";".."|] |> equal "\\foo\\bar\\baz\\asdf"
      testCase "POSIX" <| fun _ -> if not isPosix then testPassed() else path.join [|"/foo";"bar";"baz/asdf";"quux";".."|] |> equal "/foo/bar/baz/asdf"
    ]

    testList "path.normalize" [
      testCase "windows" <| fun _ -> if isPosix then testPassed() else path.normalize "C:\\temp\\\\foo\\bar\\..\\" |> equal "C:\\temp\\foo\\"
      testCase "windows" <| fun _ -> if isPosix then testPassed() else path.win32.normalize "C:////temp\\\\/\\/\\/foo/bar" |> equal "C:\\temp\\foo\\bar"
      testCase "POSIX" <| fun _ -> if not isPosix then testPassed() else path.normalize "/foo/bar//baz/asdf/quux/.." |> equal "/foo/bar/baz/asdf"
    ]

    testList "path.parse" [
      testCase "windows" <| fun _ -> 
        if isPosix then testPassed() 
        else
          let toString (parsed:PathObjectProps) = 
            sprintf "%s %s %s %s %s " parsed.root parsed.dir parsed.baseName parsed.ext parsed.name
          let pathObject = 
            jsOptions<PathObjectProps>(fun o -> 
              o.root <- "C:\\"
              o.dir <- "C:\\path\\dir"
              o.baseName <- "file.txt"
              o.ext <- ".txt"
              o.name <- "file")

          let parsed = path.parse "C:\\path\\dir\\file.txt" 
          (parsed |> toString) = (pathObject |> toString) |> equal true 


      testCase "POSIX" <| fun _ -> 
        if not isPosix then testPassed() 
        else
          let toString (parsed:PathObjectProps) = 
            sprintf "%s %s %s %s %s " parsed.root parsed.dir parsed.baseName parsed.ext parsed.name
          let pathObject = 
            jsOptions<PathObjectProps>(fun o -> 
              o.root <- "/"
              o.dir <- "/home/user/dir"
              o.baseName <- "file.txt"
              o.ext <- ".txt"
              o.name <- "file")
              
          let parsed = path.parse "/home/user/dir/file.txt"
          (parsed |> toString) = (pathObject |> toString) |> equal true 
    ]

    testList "path.relative" [
      testCase "windows" <| fun _ -> if isPosix then testPassed() else path.relative("C:\\orandea\\test\\aaa", "C:\\orandea\\impl\\bbb") |> equal "..\\..\\impl\\bbb"
      testCase "POSIX" <| fun _ -> if not isPosix then testPassed() else path.relative("/data/orandea/test/aaa", "/data/orandea/impl/bbb") |> equal "../../impl/bbb"
    ]

    testList "path.resolve & path.sep" [
      testCase "all platforms" <| fun _ ->
        let separatorChar = System.Convert.ToChar path.sep 
        let wanted = [|"foo";"bar";"baz"|] 
        path.resolve("/foo/bar", "baz")
          .Split(separatorChar) 
          |> Array.filter (fun x ->  wanted |> Array.contains x)
          |> equal wanted
    ]
  ]