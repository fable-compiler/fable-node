namespace rec Node.Cluster

open Fable.Core
open Fable.Core.DynamicExtensions
open Node.Base

module WorkersHelper = 
  let getWorkers (o:Workers) : string seq = 
      upcast JS.Object.keys(o)

  let getWorker (o:Workers) (name:string) : Worker =
      unbox o.[name]

type Workers = obj

type Worker = 
  abstract send: message: obj * ?handle:obj * ?cb: ('FIn->'FOut) -> bool
  abstract ``process``: Node.ChildProcess.ChildProcess with get
  abstract kill: signal:string -> unit
  abstract isDead: bool with get
  abstract isConnected: bool with get
  abstract exitedAfterDisconnect: bool with get
  abstract id: int with get
  abstract disconnect: unit -> Worker
  abstract on: ev: string * listener: ('a -> unit) -> Node.Events.EventEmitter
  abstract on: ev: string * listener: ('a -> 'b -> unit) -> Node.Events.EventEmitter
  abstract on: ev: string * listener: ('a -> 'b -> 'c -> unit) -> Node.Events.EventEmitter

type Settings = 
  abstract execArgv: string array with get, set 
  abstract exec: string with get, set 
  abstract args: string array with get, set 
  abstract cwd: string with get, set 
  abstract silent: bool with get, set 
  abstract uid: int with get, set 
  abstract gid: int with get, set 
  abstract inspectPort: int with get, set 
  abstract windowsHide: bool with get, set

type IExports = 
  abstract disconnect : ?cb: ('FIn->'FOut) -> unit
  abstract fork: ?env:obj -> Worker
  abstract isMaster : bool with get
  abstract isWorker : bool with get
  abstract schedulingPolicy : string with get
  abstract settings: Settings with get 
  abstract setupMaster: ?settings:Settings -> unit
  abstract worker: Worker with get
  abstract workers: Workers with get
  abstract on: ev: string * listener: ('a -> unit) -> Node.Events.EventEmitter
  abstract on: ev: string * listener: ('a -> 'b -> unit) -> Node.Events.EventEmitter
  abstract on: ev: string * listener: ('a -> 'b -> 'c -> unit) -> Node.Events.EventEmitter


