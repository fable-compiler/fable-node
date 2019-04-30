namespace rec Node.Performance

open Fable.Core
open Node.Base
open Fable.Core.JsInterop

[<StringEnum>]
type PerformanceEntryType = 
    | Node
    | Mark
    | Measure
    | Gc
    | Function
    | Http2

type [<AllowNullLiteral>] PerformanceEntry =
    abstract duration : float with get
    abstract name : string with get
    abstract startTime : float with get
    abstract entryType : PerformanceEntryType with get
    abstract kind : float option with get

type [<AllowNullLiteral>] PerformanceNodeTiming =
    inherit PerformanceEntry
    abstract bootstrapComplete : float with get
    abstract loopExit : float with get
    abstract loopStart : float with get
    abstract nodeStart : float with get
    abstract v8Start : float with get

type PerformanceObserverOptions = 
    abstract entryTypes : string array with set
    abstract buffered : bool with set

type [<AllowNullLiteral>] PerformanceObserver =
    [<Emit("new $0($1...)")>] abstract Create: (PerformanceObserverEntryList -> PerformanceObserver -> unit) -> PerformanceObserver
    abstract disconnect : unit -> unit 
    abstract observe : options : PerformanceObserverOptions -> unit

type [<AllowNullLiteral>] PerformanceObserverEntryList =
    abstract getEntries : unit -> PerformanceEntry array
    abstract getEntriesByName : name : string * ?``type`` : string -> PerformanceEntry array
    abstract getEntriesByType : ``type`` : string -> PerformanceEntry array

type [<AllowNullLiteral>] Performance =
    abstract clearMarks: ?markName: string -> unit
    abstract mark: markName: string -> unit
    abstract ``measure``: measureName: string * ?startMarkName: string * ?endMarkName: string -> unit
    abstract nodeTiming : PerformanceNodeTiming with get
    abstract now: unit -> float
    abstract timeOrigin : float with get

type [<AllowNullLiteral>] Histogram = 
    abstract disable : unit -> bool
    abstract enable : unit -> bool
    abstract exceeds : float with get
    abstract max : float with get
    abstract mean : float with get
    abstract min : float with get
    abstract percentile : float -> float 
    //abstract percentiles : Map<float,float> with get
    abstract reset : unit -> unit
    abstract  stddev : float with get

type MonitorEventdelayOptions = 
    abstract resolution : float with set 

type PerfHooks = 
    abstract monitorEventLoopDelay : MonitorEventdelayOptions -> Histogram