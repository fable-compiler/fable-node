module Tests.OS

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.DynamicExtensions
open Node.OS
open Util

let Os = Node.Api.os

let tests : Test = 
  testList "OS" [
  
    testList "os.EOL" [
      testCase "Windows" <| fun _ ->
        if isPosix then testPassed()
        else
          Os.EOL |> equal "\r\n"

      testCase "POSIX" <| fun _ ->
        if not isPosix then testPassed()
        else
          Os.EOL |> equal "\n"
    ]
    
    testList "os.arch" [
      testCase "arch" <| fun _ ->
        printfn "%s" (string (Os.arch()))
        (string (Os.arch())).Length > 0 |> equal true
    ]

    (*
    testList "os.constants" [
      testCase "os.constants" <| fun _ ->
        printfn "%s" (Os?constants )
        testPassed // need to determine how it works
    ]
    *)

    testList "os.cpus" [
      testCase "cpus" <| fun _ ->
        let ok = 
          let cpuList = Os.cpus()
          let first = cpuList |> Seq.head
          printfn "%s" (first.model)
          first.model.Length > 0
          && first.speed > 0.
          && first.times.user >= 0.
          && first.times.nice >= 0.
          && first.times.sys >= 0.
          && first.times.idle >= 0.
          && first.times.irq >= 0.

        ok |> equal true
    ]

    testList "os.endianness" [
      testCase "endianness" <| fun _ ->
        printfn "%s" (string (Os.endianness()))
        (string (Os.endianness())).Length > 0 |> equal true
    ]

    testList "os.freemem" [
      testCase "freemem" <| fun _ ->
        printfn "%f" (Os.freemem())
        Os.freemem() > 0. |> equal true // hopefully ;)
    ]

    testList "os.getPriority" [
      testCase "getPriority" <| fun _ ->
        printfn "p=%i" (Os.getPriority 0)
        testPassed()
    ]

    testList "os.homedir" [
      testCase "homedir" <| fun _ ->
        printfn "%s" (Os.homedir())
        Os.homedir().Length >0  |> equal true
    ]

    testList "os.hostname" [
      testCase "hostname" <| fun _ ->
        printfn "%s" (Os.hostname())
        Os.hostname().Length >0  |> equal true
    ]

    testList "os.loadavg" [
      testCase "loadavg" <| fun _ ->
        printfn "%A" (Os.loadavg())
        let m1, m5, m15 = Os.loadavg()
        let ok = m1 >= 0. && m5 >= 0. && m15 >=0.
        ok |> equal true
    ]

    testList "os.networkInterfaces" [
      testCase "networkInterfaces" <| fun _ ->
        let data = Os.networkInterfaces()
        let keys = data |> Node.OS.NetworkInterfaceHelper.getInterfaceNames
        keys 
          |> Seq.iter( fun key -> 
            let infos = Node.OS.NetworkInterfaceHelper.getInterfaceInfo data key
            infos |> Seq.iter( fun info -> printfn "%s:%s" key info.address )            
          ) 
        testPassed()
    ]    

    testList "os.platform" [
      testCase "platform" <| fun _ ->
        let v = string (Os.platform())
        printfn "%s" v
        v.Length > 0  |> equal true
    ]

    testList "os.release" [
      testCase "release" <| fun _ ->
        let v = string (Os.release())
        printfn "%s" v
        v.Length > 0  |> equal true
    ]

    testList "os.tmpdir" [
      testCase "tmpdir" <| fun _ ->
        let v = string (Os.tmpdir())
        printfn "%s" v
        v.Length > 0  |> equal true
    ]    

    testList "os.totalmem" [
      testCase "totalmem" <| fun _ ->
        let v = string (Os.totalmem())
        printfn "%s" v
        Os.totalmem() <> 0  |> equal true
    ]        

    testList "os.type" [
      testCase "type" <| fun _ ->
        let v = string (Os.``type``())
        printfn "%s" v
        v.Length > 0  |> equal true
    ]    

    testList "os.uptime" [
      testCase "uptime" <| fun _ ->
        let v = string (Os.uptime())
        printfn "%s" v
        Os.uptime() <> 0  |> equal true
    ]

    testList "os.userInfo" [
      testCase "userInfo" <| fun _ ->
        let info = Os.userInfo()
        printfn "%s" info.homedir
        testPassed()
    ]    
  ]