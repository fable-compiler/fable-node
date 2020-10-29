namespace rec Node.WorkerThreads

open Node
open Fable.Core
open Fable.Core.JS

type MessageChannel = 
    abstract port1: MessagePort with get
    abstract port2: MessagePort with get

type MessagePort =
    inherit Events.EventEmitter
    abstract close: unit -> unit
    abstract postMessage: message: obj -> unit
    abstract postMessage: message: obj * transferList: ResizeArray<obj> -> unit
    abstract ref: unit -> unit
    abstract start: unit -> unit
    abstract unref: unit -> unit

type ResourceLimitOptions =
    abstract member maxYoungGenerationSizeMb: float with get
    abstract member maxOldGenerationSizeMb: float with get
    abstract member codeRangeSizeMb: float with get
    abstract member stackSizeMb: float with get

type Worker = 
    inherit Events.EventEmitter
    abstract getHeapSnapshot: unit -> Promise<obj>
    abstract postMessage: value: obj -> unit
    abstract postMessage: value: obj * transferList: ResizeArray<obj> -> unit
    abstract ref: unit
    abstract resourceLimits: ResourceLimitOptions with get
    abstract stderr: Stream.Readable<obj> with get
    abstract stdin: Stream.Writable<obj> with get
    abstract stdout: Stream.Readable<obj> with get
    abstract terminate: unit -> unit
    abstract threadId: int with get
    abstract unref: unit -> unit

type [<AllowNullLiteral>] MessageChannelStatic = 
    [<Emit("new $0()")>] abstract Create: unit -> MessageChannel

type [<AllowNullLiteral>] WorkerStatic = 
    [<Emit("new $0($1)")>] abstract Create: filename: string -> Worker
    /// check https://nodejs.org/docs/latest-v12.x/api/worker_threads.html#worker_threads_new_worker_filename_options for the options object
    [<Emit("new $0($1, $2)")>] abstract Create: filename: string * options: obj -> Worker

type IExports =
    abstract isMainThread: bool with get
    abstract markAsUntransferable: obj -> unit
    abstract moveMessagePortToContext: port: MessagePort * contextifiedSandbox: obj -> MessagePort
    abstract parentPort: MessagePort with get
    abstract receiveMessageOnPort: port: MessagePort -> obj
    abstract resourceLimits: obj with get
    abstract SHARE_ENV: string with get
    abstract threadId: int with get
    abstract workerData: obj with get
    abstract Worker: WorkerStatic with get, set
    abstract MessageChannel: MessageChannelStatic with get, set
