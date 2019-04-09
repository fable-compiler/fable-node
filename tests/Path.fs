module Tests.Path

open System
open Fable.Core
open Fable.Core.JsInterop
open Node.Path

let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)

[<Global>]
let it (msg: string) (f: unit->unit): unit = jsNative

[<Global("it")>]
let itSync (msg: string) (f: unit->unit): unit = jsNative

[<Global>]
let describe (msg: string) (f: unit->unit): unit = jsNative

type DisposableAction(f) =
    interface IDisposable with
        member __.Dispose() = f()

describe "tests" <| fun _ ->
  
  describe "path.basename" <| fun _ ->
    let path = Node.Api.path
    let p = Node.Api.``process``.platform
    match p with 
    | Node.Base.Platform.Win32 -> 
      it "Windows" <| fun () ->
          path.basename "c:\\windows\\command.com"
            |> equal "command.com"

      it "win32" <| fun () ->
          path.win32.basename "c:\\windows\\command.com"
            |> equal "command.com"        
    | _ -> //posix
      it "POSIX" <| fun () ->
          path.basename "c:\\windows\\command.com" 
            |> equal "c:\\windows\\command.com"

      it "posix" <| fun () ->
          path.posix.basename "/tmp/index.html"
            |> equal "index.htdml"                

  describe "path.delimiter" <| fun _ ->
    let path = Node.Api.path
    let p = Node.Api.``process``.platform
    match p with 
    | Node.Base.Platform.Win32 -> 
      it "Windows" <| fun () -> path.delimiter |> equal ";"
    | _ -> //posix
      it "POSIX" <| fun () -> path.delimiter |> equal ":"

  describe "path.dirname" <| fun _ ->
    let path = Node.Api.path
    it "returns directory name" <| fun () -> path.dirname "/foo/bar/baz/asdf/quux" |> equal "/foo/bar/baz/asdf"

  describe "path.extname" <| fun _ ->
    let path = Node.Api.path
    it "returns the extension of the path" <| fun () -> path.extname "index.html" |> equal ".html"
    it "returns the extension of the path" <| fun () -> path.extname "index.coffee.md" |> equal ".md"
    it "returns the extension of the path" <| fun () -> path.extname "index." |> equal "."
    it "returns the extension of the path" <| fun () -> path.extname "index" |> equal ""
    it "returns the extension of the path" <| fun () -> path.extname ".index" |> equal ""

  describe "path.format" <| fun _ ->
    let path = Node.Api.path
    let p = Node.Api.``process``.platform
    match p with 
    | Node.Base.Platform.Win32 -> 
      it "Windows" <| fun () -> 
        let options = jsOptions<Node.Path.PathObjectProps>(fun o -> 
          o.dir <- "C:\\path\\dir"
          o.baseName <- "file.txt" // `base` in javascript
        )
        path.format options |> equal "C:\\path\\dir\\file.txt"

    | _ -> //posix
      it "POSIX" <| fun () -> 
        let options = jsOptions<Node.Path.PathObjectProps>(fun o -> 
          o.root <- "ignored"
          o.dir <- "/home/user/dir"
          o.baseName <- "file.txt" // `base` in javascript
        )
        path.format options |> equal "/home/user/dir/file.txt"

  describe "path.absolute" <| fun _ ->
    let path = Node.Api.path
    let p = Node.Api.``process``.platform
    let test p result = path.isAbsolute p |> equal result
    match p with 
    | Node.Base.Platform.Win32 -> 
      
      it "Windows" <| fun () -> test "//server" true
      it "Windows" <| fun () -> test "\\\\server" true
      it "Windows" <| fun () -> test "C:/foo/.." true
      it "Windows" <| fun () -> test "C:\\foo\\.." true
      it "Windows" <| fun () -> test "bar\\baz" false
      it "Windows" <| fun () -> test "bar/baz" false
      it "Windows" <| fun () -> test "." false

    | _ -> //posix
      it "POSIX" <| fun () -> test "/foo/bar" true
      it "POSIX" <| fun () -> test "/baz/.." true
      it "POSIX" <| fun () -> test "qux/.." false
      it "POSIX" <| fun () -> test "." false

  describe "path.join" <| fun _ ->
    let path = Node.Api.path
    let p = Node.Api.``process``.platform
    match p with 
    | Node.Base.Platform.Win32 -> 
      it "windows" <| fun () -> path.join [|"/foo";"bar";"baz/asdf";"quux";".."|] |> equal "\\foo\\bar\\baz\\asdf"
    | _ -> //posix
      it "posix" <| fun () -> path.join [|"/foo";"bar";"baz/asdf";"quux";".."|] |> equal "/foo/bar/baz/asdf"

  describe "path.normalize" <| fun _ ->
    let path = Node.Api.path
    let p = Node.Api.``process``.platform
    match p with 
    | Node.Base.Platform.Win32 -> 
      it "windows" <| fun () -> path.normalize "C:\\temp\\\\foo\\bar\\..\\" |> equal "C:\\temp\\foo\\"
      it "windows" <| fun () -> path.win32.normalize "C:////temp\\\\/\\/\\/foo/bar" |> equal "C:\\temp\\foo\\bar"
    | _ -> //posix
      it "posix" <| fun () -> path.normalize "/foo/bar//baz/asdf/quux/.." |> equal "/foo/bar/baz/asdf"

  describe "path.parse" <| fun _ ->
    let toString (parsed:PathObjectProps) = 
      sprintf "%s %s %s %s %s " parsed.root parsed.dir parsed.baseName parsed.ext parsed.name

    let path = Node.Api.path
    let p = Node.Api.``process``.platform
    match p with 
    | Node.Base.Platform.Win32 -> 
      it "windows" <| fun () -> 
        let pathObject = 
          jsOptions<PathObjectProps>(fun o -> 
            o.root <- "C:\\"
            o.dir <- "C:\\path\\dir"
            o.baseName <- "file.txt"
            o.ext <- ".txt"
            o.name <- "file")

        let parsed = path.parse "C:\\path\\dir\\file.txt" 
        (parsed |> toString) = (pathObject |> toString) |> equal true 
    | _ -> //posix
      it "posix" <| fun () -> 
        let pathObject = 
          jsOptions<PathObjectProps>(fun o -> 
            o.root <- "/"
            o.dir <- "/home/user/dir"
            o.baseName <- "file.txt"
            o.ext <- ".txt"
            o.name <- "file")
            
        let parsed = path.parse "/home/user/dir/file.txt"
        (parsed |> toString) = (pathObject |> toString) |> equal true 

  describe "path.relative" <| fun _ ->
    let path = Node.Api.path
    let p = Node.Api.``process``.platform
    match p with 
    | Node.Base.Platform.Win32 -> 
      it "windows" <| fun () -> path.relative("C:\\orandea\\test\\aaa", "C:\\orandea\\impl\\bbb") |> equal "..\\..\\impl\\bbb"
    | _ -> //posix
      it "posix" <| fun () -> path.relative("/data/orandea/test/aaa", "/data/orandea/impl/bbb") |> equal "../../impl/bbb"

  describe "path.resolve & path.sep" <| fun _ ->
    let path = Node.Api.path
    let separatorChar = System.Convert.ToChar path.sep 
    it "all platforms" <| fun () ->
      let wanted = [|"foo";"bar";"baz"|] 
      path.resolve("/foo/bar", "baz")
        .Split(separatorChar) 
        |> Array.filter (fun x ->  wanted |> Array.contains x)
        |> equal wanted
