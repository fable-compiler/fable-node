namespace rec Node.Process

open System
open Fable.Core
open Fable.Core.JS
open Node.Base
open Node.Events
open Node.Stream

type [<AllowNullLiteral>] Process =
      //inherit EventEmitter
      abstract stdout: Writable<string> with get, set
      abstract stderr: Writable<string> with get, set
      abstract stdin: Readable<string> with get, set
      abstract argv: ResizeArray<string> with get, set
      abstract argv0: string with get, set
      abstract execArgv: ResizeArray<string> with get, set
      abstract execPath: string with get, set
      abstract env: obj with get, set
      abstract exitCode: float with get, set
      abstract version: string with get, set
      abstract config: obj with get, set
      abstract pid: float with get, set
      abstract title: string with get, set
      abstract arch: Arch with get, set
      abstract platform: Platform with get, set
      abstract connected: bool with get, set
      abstract abort: unit -> unit
      abstract chdir: directory: string -> unit
      abstract cwd: unit -> string
      abstract exit: ?code: int -> unit
      abstract getgid: unit -> float
      abstract setgid: id: float -> unit
      abstract setgid: id: string -> unit
      /// The process.getuid() method returns the numeric user identity of the process. (See getuid(2).)
      abstract getuid: unit -> int
      /// The process.geteuid() method returns the numerical effective user identity of the process. (See geteuid(2).)
      abstract geteuid: unit -> int
      abstract setuid: id: float -> unit
      abstract setuid: id: string -> unit
      abstract kill: pid: float * ?signal: U2<string, float> -> unit
      abstract nextTick: callback: ('FIn->'FOut) * [<ParamArray>] args: obj[] -> unit
      abstract umask: ?mask: float -> float
      abstract uptime: unit -> float
      abstract hrtime: ?time: float * float -> float * float
      abstract send: message: obj * ?sendHandle: obj -> unit
      abstract disconnect: unit -> unit