namespace rec Node.Events

open System
open Fable.Core.JS
open Fable.Core

type [<AllowNullLiteral>] EventEmitter =
    abstract defaultMaxListeners: float with get, set
    abstract addListener: ev: string * listener: ('FIn->'FOut) -> EventEmitter
    abstract on: ev: string * listener: ('a -> unit) -> EventEmitter
    abstract on: ev: string * listener: ('a -> 'b -> unit) -> EventEmitter
    abstract once: ev: string * listener: ('a -> unit) -> EventEmitter
    abstract once: ev: string * listener: ('a -> 'b -> unit) -> EventEmitter
    abstract prependListener: ev: string * listener: ('FIn->'FOut) -> EventEmitter
    abstract prependOnceListener: ev: string * listener: ('FIn->'FOut) -> EventEmitter
    abstract removeListener: ev: string * listener: ('FIn->'FOut) -> EventEmitter
    abstract removeAllListener: ev: string * listener: ('FIn->'FOut) -> EventEmitter
    abstract setMaxListeners: n: int -> EventEmitter
    abstract getMaxListeners: unit -> int
    abstract listeners: ev: string -> ResizeArray<('FIn->'FOut)>
    abstract emit: ev: string * [<ParamArray>] args: obj[] -> bool
    abstract eventNames: unit -> ResizeArray<string>

type [<AllowNullLiteral>] EventEmitterStatic =
    [<Emit("new $0()")>] abstract Create: unit -> EventEmitter

type IExports =
    abstract defaultMaxListeners: float with get, set
    abstract EventEmitter: EventEmitterStatic with get, set