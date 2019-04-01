namespace rec Node.ChildProcess

open Fable.Core
open Node.Base
open Node.Buffer
open Node.Events
open Node.Stream

type ExecError =
    inherit Error
    abstract code: int with get, set
    abstract signal: string option with get, set

type [<AllowNullLiteral>] ChildProcess =
    inherit EventEmitter
    abstract stdin: Writable<string> with get, set
    abstract stdout: Readable<string> with get, set
    abstract stderr: Readable<string> with get, set
    abstract connected: bool with get, set
    abstract stdio: U2<Readable<string>,Writable<string>>[] with get, set
    abstract pid: float with get, set
    abstract kill: ?signal: string -> unit
    abstract send: message: obj * ?sendHandle: obj -> unit
    abstract disconnect: unit -> unit
    abstract unref: unit -> unit

type [<AllowNullLiteral>] ChildProcessStatic =
    [<Emit("new $0()")>] abstract Create: unit -> ChildProcess

type [<AllowNullLiteral>] ExecOptions =
    /// Default: 'utf8'
    abstract encoding : string option with get, set

    /// Current working directory of the child process
    abstract cwd: string option with get, set

    /// Timeout, in milliseconds. Default is zero (no timeout).
    abstract timeout: int option with get, set

type IExports =
    abstract ChildProcess: ChildProcessStatic with get, set
    abstract spawn: command: string * ?args: ResizeArray<string> * ?options: obj -> ChildProcess
    abstract exec: command: string * ?options: ExecOptions * ?callback:(ExecError option -> U2<string, Buffer> -> U2<string, Buffer> -> unit) -> ChildProcessStatic

    abstract execFile: file: string * ?callback: (ExecError option -> Buffer -> Buffer -> unit) -> ChildProcess

    abstract execFile: file: string * ?args: ResizeArray<string> * ?callback: (ExecError option -> Buffer -> Buffer -> unit) -> ChildProcess

    abstract execFile :file: string * ?args: ResizeArray<string> * ?options: ExecOptions * ?callback: (ExecError option -> Buffer -> Buffer -> unit) -> ChildProcess

    abstract fork: modulePath: string * ?args: ResizeArray<string> * ?options: obj -> ChildProcess

    abstract execSync: command: string * ?options: obj -> U2<string, Buffer>

    abstract execFileSync: command: string * ?args: ResizeArray<string> * ?options: obj -> U2<string, Buffer>

    abstract spawnSync: command: string * ?args: ResizeArray<string> * ?options: obj -> obj