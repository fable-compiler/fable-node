namespace rec Node.Dgram

open Node
open Node.Buffer
open Fable.Core


type [<AllowNullLiteral>] AddressInfo = 
    abstract address: string with get
    abstract port: int with get
    abstract family: string with get

type [<AllowNullLiteral>] BindOptions = 
    abstract port: int with get, set
    abstract address: string with get, set
    abstract exclusive: bool with get, set
    abstract fd: int with get, set

type [<AllowNullLiteral>] CreateSocketOptions =
    abstract ``type``: string with get, set
    abstract reuseAddr: bool with get, set
    abstract ipv6Only: bool with get, set
    abstract recvBufferSize: int with get, set
    abstract sendBufferSize : int with get, set
    abstract lookup: (string -> obj -> (Node.Base.Error option -> string option -> int option -> unit)) option with get, set

type [<AllowNullLiteral>] Socket =
    inherit Events.EventEmitter
    abstract addMembership: multicastAddress: string  * ?multicastInterface: string -> unit
    abstract addSourceSpecificMembership: sourceAddress: string * groupAddress: string * ?multicastInterface: string -> unit
    abstract address: unit -> AddressInfo
    abstract bind: ?port: int * ?address: string * ?callback: (unit -> unit) -> unit
    abstract bind: ?options: BindOptions * ?callback: (unit -> unit) -> unit
    abstract close: ?callback: (unit -> unit) -> unit
    abstract connect: port: int * ?address: string * ?callback: (unit -> unit) -> unit
    abstract disconnect: unit -> unit
    abstract dropMembership: multicastAddress: string * ?multicastInterface: string -> unit
    abstract dropSourceSpecificMembership: sourceAddress: string * groupAddress: string * ?multicastInterface: string -> unit
    abstract getRecvBufferSize: unit -> int
    abstract getSendBufferSize: unit -> int
    abstract ref: unit -> Dgram.Socket
    abstract remoteAddress: unit -> AddressInfo
    abstract send: Buffer * ?offset: int * ?length: int * ?port: int * ?address: string * ?callback: ('FIn -> unit) -> int
    abstract send: string * ?offset: int * ?length: int * ?port: int * ?address: string * ?callback: ('FIn -> unit) -> int
    abstract send: byte[] * ?offset: int * ?length: int * ?port: int * ?address: string * ?callback: ('FIn -> unit) -> int
    abstract send: ResizeArray<obj> * ?offset: int * ?length: int * ?port: int * ?address: string * ?callback: ('FIn -> unit) -> int
    abstract setBroadcast: bool -> unit
    abstract setMulticastInterface: multicastInterface: string -> unit
    abstract setMulticastLoopback: flag: bool -> unit
    abstract setMulticastTTL: ttl: int -> unit
    abstract setRecvBufferSize: size: int -> unit
    abstract setSendBufferSize: size: int -> unit
    abstract setTTL: ttl: int -> unit
    abstract unref: unit -> unit

type IExports = 
    abstract createSocket: ``type``: string * ?callback: (unit -> unit) -> Dgram.Socket
    abstract createSocket: options: CreateSocketOptions * ?callback: (unit -> unit) -> Dgram.Socket

