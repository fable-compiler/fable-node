module Tests.TraceEvents

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.DynamicExtensions
open Node.Performance
open Util

let trace_events = Node.Api.TraceEVents

let tests : Test = 
  testList "Trace event" [
    testList "overall" [    
      testCase "non buffered" <| fun _ ->
        let opt = jsOptions<Node.TraceEvents.Options>( fun o -> o.categories <- [|"node";"v8"|])
        let opt2 = jsOptions<Node.TraceEvents.Options>( fun o -> o.categories <- [|"node.perf";"node"|])
        let t1 = trace_events.createTracing opt
        let t2 = trace_events.createTracing opt2 
        t1.enable()
        t2.enable()
        JS.console.log (trace_events.getEnabledCategories())
        t2.disable()
        JS.console.log (trace_events.getEnabledCategories())
        testPassed()    
    ]    
  ]
