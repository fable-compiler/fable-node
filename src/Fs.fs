namespace rec Node.Fs

open System
open Fable.Core
open Node.Base
open Node.Stream
open Node.Events
open Node.Buffer

type [<AllowNullLiteral>] Stats =
    abstract dev: float with get, set
    abstract ino: float with get, set
    abstract mode: float with get, set
    abstract nlink: float with get, set
    abstract uid: float with get, set
    abstract gid: float with get, set
    abstract rdev: float with get, set
    abstract size: float with get, set
    abstract blksize: float with get, set
    abstract blocks: float with get, set
    abstract atime: DateTime with get, set
    abstract mtime: DateTime with get, set
    abstract ctime: DateTime with get, set
    abstract birthtime: DateTime with get, set
    abstract isFile: unit -> bool
    abstract isDirectory: unit -> bool
    abstract isBlockDevice: unit -> bool
    abstract isCharacterDevice: unit -> bool
    abstract isSymbolicLink: unit -> bool
    abstract isFIFO: unit -> bool
    abstract isSocket: unit -> bool

type [<StringEnum>] SymlinkType =
    | Dir | File | Junction

type [<StringEnum>] FsWatcherEncoding =
    | Utf8 | Buffer

type [<AllowNullLiteral>] FsWatcherOptions =
    abstract persistent: bool option with get, set
    abstract recursive: bool option with get, set
    abstract encoding: FsWatcherEncoding option with get, set

type [<AllowNullLiteral>] FSWatcher =
    inherit EventEmitter
    abstract close: unit -> unit

type [<AllowNullLiteral>] ReadStreamOptions =
    abstract flags: string option with get, set
    abstract encoding: string option with get, set
    abstract fd: int option with get, set
    abstract mode: int option with get, set
    abstract autoClose: bool option with get, set
    abstract start: int option with get, set
    abstract ``end``: int option with get, set

type [<AllowNullLiteral>] ReadStream<'a> =
    inherit Readable<'a>
    abstract bytesRead: float with get, set
    abstract path: 'a with get, set
    abstract close: unit -> unit
    abstract destroy: unit -> unit

type [<AllowNullLiteral>] ReadStreamStatic =
    [<Emit("new $0($1, $2)")>] abstract Create: path:string * opts:ReadStreamOptions option -> ReadStream<string>
    [<Emit("new $0($1, $2)")>] abstract Create: path:Buffer * opts:ReadStreamOptions option -> ReadStream<Buffer>

type [<AllowNullLiteral>] WriteStreamOptions =
    abstract flags: string option with get, set
    abstract defaultEncoding: string option with get, set
    abstract fd: int option with get, set
    abstract mode: int option with get, set
    abstract autoClose: bool option with get, set
    abstract start: int option with get, set

type [<AllowNullLiteral>] WriteStream<'a> =
    inherit Writable<'a>
    abstract bytesWritten: float with get, set
    abstract path: 'a with get, set
    abstract close: unit -> unit

type [<AllowNullLiteral>] WriteStreamStatic =
    [<Emit("new $0($1, $2)")>] abstract Create: path:string * opts:WriteStreamOptions option -> WriteStream<string>
    [<Emit("new $0($1, $2)")>] abstract Create: path:Buffer * opts:WriteStreamOptions option -> WriteStream<Buffer>

type FsConstants =
    abstract F_OK: float with get, set
    abstract R_OK: float with get, set
    abstract W_OK: float with get, set
    abstract X_OK: float with get, set
    abstract O_RDONLY: float with get, set
    abstract O_WRONLY: float with get, set
    abstract O_RDWR: float with get, set
    abstract O_CREAT: float with get, set
    abstract O_EXCL: float with get, set
    abstract O_NOCTTY: float with get, set
    abstract O_TRUNC: float with get, set
    abstract O_APPEND: float with get, set
    abstract O_DIRECTORY: float with get, set
    abstract O_NOATIME: float with get, set
    abstract O_NOFOLLOW: float with get, set
    abstract O_SYNC: float with get, set
    abstract O_SYMLINK: float with get, set
    abstract O_DIRECT: float with get, set
    abstract O_NONBLOCK: float with get, set
    abstract S_IFMT: float with get, set
    abstract S_IFREG: float with get, set
    abstract S_IFDIR: float with get, set
    abstract S_IFCHR: float with get, set
    abstract S_IFBLK: float with get, set
    abstract S_IFIFO: float with get, set
    abstract S_IFLNK: float with get, set
    abstract S_IFSOCK: float with get, set
    abstract S_IRWXU: float with get, set
    abstract S_IRUSR: float with get, set
    abstract S_IWUSR: float with get, set
    abstract S_IXUSR: float with get, set
    abstract S_IRWXG: float with get, set
    abstract S_IRGRP: float with get, set
    abstract S_IWGRP: float with get, set
    abstract S_IXGRP: float with get, set
    abstract S_IRWXO: float with get, set
    abstract S_IROTH: float with get, set
    abstract S_IWOTH: float with get, set
    abstract S_IXOTH: float with get, set

type IExports =
    abstract watch: filename: string * ?listener: (string -> string -> unit) -> FSWatcher
    abstract watch: filename: string * encoding: FsWatcherEncoding * ?listener: (string -> string -> unit) -> FSWatcher
    abstract watch: filename: string * options: FsWatcherOptions * ?listener: (string -> string -> unit) -> FSWatcher
    abstract ReadStream: ReadStreamStatic with get, set
    abstract WriteStream: WriteStreamStatic with get, set
    abstract createReadStream: path: string * ?options: ReadableOptions -> ReadStream<string>
    abstract createReadStream: path: Buffer * ?options: ReadableOptions -> ReadStream<Buffer>
    abstract createWriteStream: path: string * ?options: WritableOptions<string> -> WriteStream<string>
    abstract createWriteStream: path: Buffer * ?options: WritableOptions<Buffer> -> WriteStream<Buffer>
    abstract constants: FsConstants
    abstract rename: oldPath: string * newPath: string * ?callback: (ErrnoException option -> unit) -> unit
    abstract renameSync: oldPath: string * newPath: string -> unit
    abstract truncate: path: U2<string, Buffer> * ?callback: (ErrnoException option -> unit) -> unit
    abstract truncate: path: U2<string, Buffer> * len: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract truncateSync: path: U2<string, Buffer> * ?len: float -> unit
    abstract ftruncate: fd: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract ftruncate: fd: float * len: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract ftruncateSync: fd: float * ?len: float -> unit
    abstract chown: path: U2<string, Buffer> * uid: float * gid: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract chownSync: path: U2<string, Buffer> * uid: float * gid: float -> unit
    abstract fchown: fd: float * uid: float * gid: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract fchownSync: fd: float * uid: float * gid: float -> unit
    abstract lchown: path: U2<string, Buffer> * uid: float * gid: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract lchownSync: path: U2<string, Buffer> * uid: float * gid: float -> unit
    abstract chmod: path: U2<string, Buffer> * mode: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract chmod: path: U2<string, Buffer> * mode: string * ?callback: (ErrnoException option -> unit) -> unit
    abstract chmodSync: path: U2<string, Buffer> * mode: float -> unit
    abstract chmodSync: path: U2<string, Buffer> * mode: string -> unit
    abstract fchmod: fd: float * mode: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract fchmod: fd: float * mode: string * ?callback: (ErrnoException option -> unit) -> unit
    abstract fchmodSync: fd: float * mode: float -> unit
    abstract fchmodSync: fd: float * mode: string -> unit
    abstract lchmod: path: U2<string, Buffer> * mode: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract lchmod: path: U2<string, Buffer> * mode: string * ?callback: (ErrnoException option -> unit) -> unit
    abstract lchmodSync: path: U2<string, Buffer> * mode: float -> unit
    abstract lchmodSync: path: U2<string, Buffer> * mode: string -> unit
    abstract stat: path: U2<string, Buffer> * ?callback: (ErrnoException option -> Stats -> obj) -> unit
    abstract lstat: path: U2<string, Buffer> * ?callback: (ErrnoException option -> Stats -> obj) -> unit
    abstract fstat: fd: float * ?callback: (ErrnoException option -> Stats -> obj) -> unit
    abstract statSync: path: U2<string, Buffer> -> Stats
    abstract lstatSync: path: U2<string, Buffer> -> Stats
    abstract fstatSync: fd: float -> Stats
    abstract link: srcpath: U2<string, Buffer> * dstpath: U2<string, Buffer> * ?callback: (ErrnoException option -> unit) -> unit
    abstract linkSync: srcpath: U2<string, Buffer> * dstpath: U2<string, Buffer> -> unit
    abstract symlink: srcpath: U2<string, Buffer> * dstpath: U2<string, Buffer> * ?``type``: SymlinkType * ?callback: (ErrnoException option -> unit) -> unit
    abstract symlinkSync: srcpath: U2<string, Buffer> * dstpath: U2<string, Buffer> * ?``type``: SymlinkType -> unit
    abstract readlink: path: U2<string, Buffer> * ?callback: (ErrnoException option -> string -> obj) -> unit
    abstract readlinkSync: path: U2<string, Buffer> -> string
    abstract realpath: path: U2<string, Buffer> * ?callback: (ErrnoException option -> string -> obj) -> unit
    abstract realpath: path: U2<string, Buffer> * cache: obj * callback: (ErrnoException option -> string -> obj) -> unit
    abstract realpathSync: path: U2<string, Buffer> * ?cache: obj -> string
    abstract unlink: path: U2<string, Buffer> * ?callback: (ErrnoException option -> unit) -> unit
    abstract unlinkSync: path: U2<string, Buffer> -> unit
    abstract rmdir: path: U2<string, Buffer> * ?callback: (ErrnoException option -> unit) -> unit
    abstract rmdirSync: path: U2<string, Buffer> -> unit
    /// Asynchronous mkdir(2). No arguments other than a possible exception are given to the completion callback. mode defaults to 0o777.
    abstract mkdir: path: string * ?callback: (ErrnoException option -> unit) -> unit
    abstract mkdir: path: Buffer * ?callback: (ErrnoException option -> unit) -> unit
    abstract mkdir: path: string * mode: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract mkdir: path: Buffer * mode: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract mkdir: path: string * mode: string * ?callback: (ErrnoException option -> unit) -> unit
    abstract mkdir: path: Buffer * mode: string * ?callback: (ErrnoException option -> unit) -> unit
    /// Synchronous mkdir(2). Returns undefined.
    abstract mkdirSync: path: string -> unit
    abstract mkdirSync: path: Buffer -> unit
    abstract mkdirSync: path: string * mode: float -> unit
    abstract mkdirSync: path: Buffer * mode: float -> unit
    abstract mkdirSync: path: string * mode: string -> unit
    abstract mkdirSync: path: Buffer * mode: string -> unit
    abstract mkdtemp: prefix: string * ?callback: (ErrnoException option -> string -> unit) -> unit
    abstract mkdtempSync: prefix: string -> string
    abstract readdir: path: U2<string, Buffer> * ?callback: (ErrnoException option -> ResizeArray<string> -> unit) -> unit
    abstract readdirSync: path: U2<string, Buffer> -> ResizeArray<string>
    abstract close: fd: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract closeSync: fd: float -> unit
    abstract ``open``: path: U2<string, Buffer> * flags: U2<string, float> * callback: (ErrnoException option -> float -> unit) -> unit
    abstract ``open``: path: U2<string, Buffer> * flags: U2<string, float> * mode: float * callback: (ErrnoException option -> float -> unit) -> unit
    abstract openSync: path: U2<string, Buffer> * flags: U2<string, float> * ?mode: float -> float
    abstract utimes: path: U2<string, Buffer> * atime: float * mtime: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract utimes: path: U2<string, Buffer> * atime: DateTime * mtime: DateTime * ?callback: (ErrnoException option -> unit) -> unit
    abstract utimesSync: path: U2<string, Buffer> * atime: float * mtime: float -> unit
    abstract utimesSync: path: U2<string, Buffer> * atime: DateTime * mtime: DateTime -> unit
    abstract futimes: fd: float * atime: float * mtime: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract futimes: fd: float * atime: DateTime * mtime: DateTime * ?callback: (ErrnoException option -> unit) -> unit
    abstract futimesSync: fd: float * atime: float * mtime: float -> unit
    abstract futimesSync: fd: float * atime: DateTime * mtime: DateTime -> unit
    abstract fsync: fd: float * ?callback: (ErrnoException option -> unit) -> unit
    abstract fsyncSync: fd: float -> unit
    abstract write: fd: float * buffer: Buffer * offset: float * length: float * position: float * ?callback: (ErrnoException option -> float -> Buffer -> unit) -> unit
    abstract write: fd: float * buffer: Buffer * offset: float * length: float * ?callback: (ErrnoException option -> float -> Buffer -> unit) -> unit
    abstract write: fd: float * data: obj * ?callback: (ErrnoException option -> float -> string -> unit) -> unit
    abstract write: fd: float * data: obj * offset: float * ?callback: (ErrnoException option -> float -> string -> unit) -> unit
    abstract write: fd: float * data: obj * offset: float * encoding: string * ?callback: (ErrnoException option -> float -> string -> unit) -> unit
    abstract writeSync: fd: float * buffer: Buffer * offset: float * length: float * ?position: float -> float
    abstract writeSync: fd: float * data: obj * ?position: float * ?enconding: string -> float
    abstract read: fd: float * buffer: Buffer * offset: float * length: float * position: float * ?callback: (ErrnoException option -> float -> Buffer -> unit) -> unit
    abstract readSync: fd: float * buffer: Buffer * offset: float * length: float * position: float -> float
    abstract readFile: filename: string * encoding: string * callback: (ErrnoException option -> string -> unit) -> unit
    abstract readFile: filename: string * options: obj * callback: (ErrnoException option -> string -> unit) -> unit
    abstract readFile: filename: string * options: obj * callback: (ErrnoException option -> Buffer -> unit) -> unit
    abstract readFile: filename: string * callback: (ErrnoException option -> Buffer -> unit) -> unit
    abstract readFileSync: filename: string * encoding: string -> string
    abstract readFileSync: filename: string * options: obj -> string
    abstract readFileSync: filename: string * ?options: obj -> Buffer
    abstract writeFile: filename: string * data: obj * ?callback: (ErrnoException option -> unit) -> unit
    abstract writeFile: filename: string * data: obj * options: obj * ?callback: (ErrnoException option -> unit) -> unit
    abstract writeFileSync: filename: string * data: obj * ?options: obj -> unit
    abstract appendFile: filename: string * data: obj * options: obj * ?callback: (ErrnoException option -> unit) -> unit
    abstract appendFile: filename: string * data: obj * ?callback: (ErrnoException option -> unit) -> unit
    abstract appendFileSync: filename: string * data: obj * ?options: obj -> unit
    abstract watchFile: filename: string * listener: (Stats -> Stats -> unit) -> unit
    abstract watchFile: filename: string * options: obj * listener: (Stats -> Stats -> unit) -> unit
    abstract unwatchFile: filename: string * ?listener: (Stats -> Stats -> unit) -> unit

    abstract exists: path: U2<string, Buffer> * ?callback: (bool -> unit) -> unit
    abstract existsSync: path: U2<string, Buffer> -> bool
    abstract access: path: U2<string, Buffer> * callback: (ErrnoException option -> unit) -> unit
    abstract access: path: U2<string, Buffer> * mode: float * callback: (ErrnoException option -> unit) -> unit
    abstract accessSync: path: U2<string, Buffer> * ?mode: float -> unit

    abstract fdatasync: fd: float * callback: (ErrnoException option -> unit) -> unit
    abstract fdatasyncSync: fd: float -> unit