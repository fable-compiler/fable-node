namespace rec Node.Buffer

open Fable.Core
open Fable.Core.JS
open System

(*
byte[] > UInt8Array
sbyte[] > Int8Array
ushort[] > UInt16Array
short[] > Int16Array
uint[] > UInt32Array
int[] > Int32Array
float32[] > Float32Array
float64[] > Float64Array
*)

[<StringEnum>]
type BufferEncoding =
    ///For 7-bit ASCII data only. This encoding is fast and will strip the high bit if set.
    | Ascii
    /// Multibyte encoded Unicode characters. Many web pages and other document formats use UTF-8.
    | Utf8
    /// 2 or 4 bytes, little-endian encoded Unicode characters. Surrogate pairs (U+10000 to U+10FFFF) are supported.
    | Utf16le
    /// Alias of 'utf16le'
    | Usc2
    /// Base64 encoding. When creating a Buffer from a string, this encoding will also correctly accept "URL and Filename Safe Alphabet" as specified in RFC4648, Section 5.
    | Base64
    /// A way of encoding the Buffer into a one-byte encoded string (as defined by the IANA in RFC1345, page 63, to be the Latin-1 supplement block and C0/C1 control codes)
    | Latin1
    /// Alias for 'latin1'.
    | Binary
    ///  Encode each byte as two hexadecimal characters.
    | Hex

type Buffer =
    abstract buffer: ArrayBuffer with get
    abstract byteOffset: int with get
    abstract compare: otherBuffer: Buffer * ?targetStart: int * ?targetEnd: int * ?sourceStart: int * ?sourceEnd: int -> int
    abstract copy: targetBuffer: Buffer * ?targetStart: int * ?sourceStart: int * ?sourceEnd: int -> int
    abstract entries: unit -> seq<int * int>
    abstract equals: otherBuffer: Buffer -> bool
    abstract fill: value: string * ?offset: int * ?``end``: int * ?encoding: BufferEncoding-> Buffer
    abstract fill: value: Buffer * ?offset: int * ?``end``: int * ?encoding: BufferEncoding-> Buffer
    abstract fill: value: int * ?offset: int * ?``end``: int * ?encoding: BufferEncoding-> Buffer
    abstract includes: value: string * ?byteOffset: int * ?encoding: BufferEncoding -> bool
    abstract includes: value: Buffer * ?byteOffset: int * ?encoding: BufferEncoding -> bool
    abstract includes: value: int * ?byteOffset: int * ?encoding: BufferEncoding -> bool
    abstract indexOf: value: int * ?byteOffset: int * ?encoding: BufferEncoding -> int
    abstract indexOf: value: string * ?byteOffset: int * ?encoding: BufferEncoding -> int
    abstract indexOf: value: Buffer * ?byteOffset: int * ?encoding: BufferEncoding -> int
    abstract keys: unit -> seq<int>
    abstract lastIndexOf: value: int * ?byteOffset: int * ?encoding: BufferEncoding -> int
    abstract lastIndexOf: value: string * ?byteOffset: int * ?encoding: BufferEncoding -> int
    abstract lastIndexOf: value: Buffer * ?byteOffset: int * ?encoding: BufferEncoding -> int
    abstract length: int with get
    abstract readDoubleLE: offset: int * ?noAssert: bool -> float
    abstract readDoubleBE: offset: int * ?noAssert: bool -> float
    abstract readFloatLE: offset: int * ?noAssert: bool -> float
    abstract readFloatBE: offset: int * ?noAssert: bool -> float
    abstract readInt8: offset: int * ?noAssert: bool -> int
    abstract readInt16LE: offset: int * ?noAssert: bool -> int
    abstract readInt16BE: offset: int * ?noAssert: bool -> int
    abstract readInt32LE: offset: int * ?noAssert: bool -> int32
    abstract readInt32BE: offset: int * ?noAssert: bool -> int32
    abstract readUInt8: offset: int * ?noAssert: bool -> int
    abstract readUInt16LE: offset: int * ?noAssert: bool -> int
    abstract readUInt16BE: offset: int * ?noAssert: bool -> int
    abstract readUInt32LE: offset: int * ?noAssert: bool -> int
    abstract readUInt32BE: offset: int * ?noAssert: bool -> int
    abstract readUIntLE: offset: int * byteLength: int * ?noAssert: bool -> int
    abstract readUIntBE: offset: int * byteLength: int * ?noAssert: bool -> int
    abstract readIntLE: offset: int * byteLength: int * ?noAssert: bool -> int
    abstract readIntBE: offset: int * byteLength: int * ?noAssert: bool -> int
    abstract slice: ?start: int * ?``end``: int -> Buffer
    abstract swap16: unit -> Buffer
    abstract swap32: unit -> Buffer
    abstract swap64: unit -> Buffer
    abstract toJSON: unit -> obj
    abstract toString: ?encoding: BufferEncoding * ?start: int * ?``end``: int -> string
    abstract values: unit -> seq<int>
    abstract write: string: string * ?offset: int * ?length: int * ?encoding: BufferEncoding -> int
    abstract writeIntLE: value: float * offset: int * byteLength: int * ?noAssert: bool -> float
    abstract writeIntBE: value: float * offset: int * byteLength: int * ?noAssert: bool -> float
    abstract writeUIntLE: value: float * offset: int * byteLength: int * ?noAssert: bool -> float
    abstract writeUIntBE: value: float * offset: int * byteLength: int * ?noAssert: bool -> float
    abstract writeUInt8: value: int * offset: int * ?noAssert: bool -> int
    abstract writeUInt16LE: value: int * offset: int * ?noAssert: bool -> int
    abstract writeUInt16BE: value: int * offset: int * ?noAssert: bool -> int
    abstract writeUInt32LE: value: float * offset: int * ?noAssert: bool -> float
    abstract writeUInt32BE: value: float * offset: int * ?noAssert: bool -> float
    abstract writeInt8: value: int * offset: int * ?noAssert: bool -> int
    abstract writeInt16LE: value: int * offset: int * ?noAssert: bool -> int
    abstract writeInt16BE: value: int * offset: int * ?noAssert: bool -> int
    abstract writeInt32LE: value: int * offset: int * ?noAssert: bool -> int
    abstract writeInt32BE: value: int * offset: int * ?noAssert: bool -> int
    abstract writeFloatLE: value: float * offset: int * ?noAssert: bool -> float
    abstract writeFloatBE: value: float * offset: int * ?noAssert: bool -> float
    abstract writeDoubleLE: value: float * offset: int * ?noAssert: bool -> float
    abstract writeDoubleBE: value: float * offset: int * ?noAssert: bool -> float
    abstract INSPECT_MAX_BYTES: int with get
    abstract kMaxLength:int with get 

type [<AllowNullLiteral>] BufferStatic =
    abstract alloc: size: int * ?fill: string * ?encoding: BufferEncoding -> Buffer
    abstract alloc: size: int * ?fill: Buffer * ?encoding: BufferEncoding -> Buffer
    abstract alloc: size: int * ?fill: int array * ?encoding: BufferEncoding -> Buffer
    abstract alloc: size: int * ?fill: int * ?encoding: BufferEncoding -> Buffer
    abstract alloc: size: int -> Buffer
    abstract allocUnsafe: size: int -> Buffer
    abstract allocUnsafeSlow: size: int -> Buffer
    abstract byteLength: string: string * ?encoding: BufferEncoding -> int
    abstract byteLength: string: Buffer * ?encoding: BufferEncoding -> int
    abstract byteLength: string: DataView * ?encoding: BufferEncoding -> int
    abstract byteLength: string: ArrayBuffer * ?encoding: BufferEncoding -> int
    abstract byteLength: string: obj array * ?encoding: BufferEncoding -> int
    abstract compare: buf1: Buffer * buf2: Buffer -> int
    abstract compare: buf1: byte [] * buf2: byte [] -> int
    abstract concat: list: Buffer [] * ?totalLength: int -> Buffer
    abstract concat: list: byte [] [] * ?totalLength: int -> Buffer
    abstract from: array: int [] -> Buffer
    abstract from: arrayBuffer: ArrayBuffer * ?byteOffset: int * ?length: int -> Buffer
    abstract from: object: obj * ?byteOffset: int * ?length: int -> Buffer
    abstract from: object: obj * ?byteOffset: string * ?length: int -> Buffer
    abstract from: buffer: Buffer -> Buffer
    abstract from: buffer: byte [] -> Buffer
    abstract from: str: string * ?encoding: BufferEncoding -> Buffer
    abstract isBuffer: obj: obj -> bool
    abstract isEncoding: encoding: BufferEncoding -> bool
    abstract poolSize : int with get

    [<Obsolete("Check https://nodejs.org/docs/latest/api/buffer.html#buffer_class_buffer")>][<Emit("new $0($1...)")>] abstract Create: arrayBuffer: ArrayBuffer -> Buffer
    [<Obsolete("Check https://nodejs.org/docs/latest/api/buffer.html#buffer_class_buffer.")>] [<Emit("new $0($1...)")>] abstract Create: str: string * ?encoding: BufferEncoding -> Buffer
    [<Obsolete("Check https://nodejs.org/docs/latest/api/buffer.html#buffer_class_buffer.")>] [<Emit("new $0($1...)")>] abstract Create: str: int -> Buffer
    [<Obsolete("Check https://nodejs.org/docs/latest/api/buffer.html#buffer_class_buffer.")>] [<Emit("new $0($1...)")>] abstract Create: array: byte[] -> Buffer
    [<Obsolete("Check https://nodejs.org/docs/latest/api/buffer.html#buffer_class_buffer.")>] [<Emit("new $0($1...)")>] abstract Create: array: ResizeArray<obj> -> Buffer
    [<Obsolete("Check https://nodejs.org/docs/latest/api/buffer.html#buffer_class_buffer.")>] [<Emit("new $0($1...)")>] abstract Create: buffer: Buffer -> Buffer

[<Obsolete("Check https://nodejs.org/docs/latest/api/buffer.html#buffer_class_buffer.")>]
type [<AllowNullLiteral>] SlowBuffer =
    abstract prototype: Buffer with get, set
    abstract isBuffer: obj: obj -> bool
    abstract byteLength: string: string * ?encoding: BufferEncoding -> int
    abstract concat: list: ResizeArray<Buffer> * ?totalLength: int -> Buffer

[<Obsolete("Check https://nodejs.org/docs/latest/api/buffer.html#buffer_class_buffer.")>]
type [<AllowNullLiteral>] SlowBufferStatic =
    [<Emit("new $0($1...)")>] abstract Create: str: string * ?encoding: BufferEncoding -> Buffer
    [<Emit("new $0($1...)")>] abstract Create: str: int -> Buffer
    [<Emit("new $0($1...)")>] abstract Create: array: byte[] -> Buffer
    [<Emit("new $0($1...)")>] abstract Create: array: ResizeArray<obj> -> Buffer

type BufferConstants = 
    abstract MAX_LENGTH: int with get
    abstract MAX_STRING_LENGTH: int with get

type IExports =
    abstract Buffer: BufferStatic with get, set

    [<Obsolete("Check https://nodejs.org/docs/latest/api/buffer.html#buffer_class_buffer.")>]
    abstract SlowBuffer: SlowBufferStatic with get, set
    abstract constants: BufferConstants with get
    abstract transcode: source:Buffer  * fromEnc:BufferEncoding * toEnc:BufferEncoding -> Buffer
