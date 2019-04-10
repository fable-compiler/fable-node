module Tests.Util

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.Testing
open Node.OS

let testList (name : string) (tests: seq<'b>) = name, tests
let testCase (msg : string) (test : obj -> unit) = msg, test

let equal expected actual: unit =
    Assert.AreEqual(actual, expected)

type Test = string * seq<string * seq<string * (obj -> unit)>>        

let isPosix = 
  let p = Node.Api.``process``.platform
  match p with 
  | Node.Base.Platform.Win32 -> false
  | _ -> true

let testPassed() = true |> equal true
let testFailed() = false |> equal true
