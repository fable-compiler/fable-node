module Tests

open System
open Fable.Core
open Fable.Core.JsInterop

let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)

[<Global>]
let it (msg: string) (f: unit->bool): unit = jsNative

[<Global("it")>]
let itSync (msg: string) (f: unit->unit): unit = jsNative

[<Global>]
let describe (msg: string) (f: unit->unit): unit = jsNative

type DisposableAction(f) =
    interface IDisposable with
        member __.Dispose() = f()

describe "tests" <| fun _ ->
    it "Simple tests " <| fun () ->
        true