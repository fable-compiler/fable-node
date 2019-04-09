module Tests

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
  
  describe "Path" <| fun _ ->
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