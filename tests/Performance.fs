module Tests.Performance

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.DynamicExtensions
open Node.Performance
open Util

let performance = Node.Api.performance
let PerformanceObserver = Node.Api.PerformanceObserver

let tests : Test = 
  testList "Performance" [
  
    testList "overall" [
      testCase "buffered" <| fun _ ->
        
        let obs : PerformanceObserver = PerformanceObserver.Create (fun list observer -> 
          list.getEntries()
            |> Seq.iter ( fun entry -> 
              JS.console.log entry.duration
              JS.console.log entry.entryType
              JS.console.log entry.name
              JS.console.log entry.startTime
            )
          JS.console.log (list.getEntriesByName "test1")
          observer.disconnect()
          list.getEntries() |> Seq.length |> equal 3
        )
        let options = jsOptions<PerformanceObserverOptions>( fun opt -> 
          opt.entryTypes <- [|"mark"|]
          opt.buffered <- true
        )
        obs.observe options
        [0..2] |> Seq.iter( fun i -> performance.mark (sprintf "test%i" i ) )
        //testPassed()

      testCase "non buffered" <| fun _ ->
        
        let obs : PerformanceObserver = PerformanceObserver.Create (fun list observer -> 
          list.getEntries()
            |> Seq.iter ( fun entry -> 
              JS.console.log entry.duration
              JS.console.log entry.entryType
              JS.console.log entry.name
              JS.console.log entry.startTime
            )
          JS.console.log (list.getEntriesByName "test1")
          observer.disconnect()        
          list.getEntries() |> Seq.length |> equal 3
        )
        let options = jsOptions<PerformanceObserverOptions>( fun opt -> 
          opt.entryTypes <- [|"mark"|]
          opt.buffered <- false
        )
        obs.observe options
        [0..2] |> Seq.iter( fun i -> performance.mark (sprintf "test%i" i ) )
        testPassed()

      testCase "nodeTiming" <| fun _ ->
        let timing = performance.nodeTiming
        JS.console.log timing.duration
        JS.console.log timing.bootstrapComplete
        JS.console.log timing.entryType
        JS.console.log timing.kind
        JS.console.log timing.loopExit
        JS.console.log timing.loopStart
        JS.console.log timing.name
        JS.console.log timing.nodeStart
        JS.console.log timing.startTime
        JS.console.log timing.v8Start
        testPassed()
    ]
  ]