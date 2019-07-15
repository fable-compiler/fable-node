namespace rec Node.TraceEvents

open Fable.Core
open Fable.Core.DynamicExtensions
open Node.Base

type Options = 
    abstract categories: string array with set
 
type Tracing = 
    // A comma-separated list of the trace event categories covered by this Tracing object.
    abstract categories: string 
    // Enables this Tracing object for the set of categories covered by the Tracing object.
    abstract enable: unit -> unit
    // Disables this Tracing object.. Only trace event categories not covered by other enabled Tracing objects and not specified by the --trace-event-categories flag will be disabled.
    abstract disable: unit -> unit
    //true only if the Tracing object has been enabled.
    abstract enabled: bool

type IExports =
    // Creates and returns a Tracing object for the given set of categories.
    abstract createTracing: options:Options -> Tracing
    // Returns a comma-separated list of all currently-enabled trace event categories. The current set of enabled trace event categories is determined by the union of all currently-enabled Tracing objects and any categories enabled using the --trace-event-categories flag.
    abstract getEnabledCategories : unit -> string array
