namespace rec Node.Url

open Fable.Core
open Node.Base

// todo: check Static.format can't make it work. See commented sample in tests

type IFormatOptions = 
    abstract auth:bool option with get, set
    abstract fragment:bool option with get, set
    abstract search:bool option with get, set
    abstract unicode:bool option with get, set

type [<AllowNullLiteral>] URLSearchParams = 
    [<Emit("new $0($1...)")>] 
    abstract Create: input:U3<unit, string,URLSearchParams> -> URLSearchParams
    abstract append: string * string -> unit
    abstract delete: string -> unit
    abstract get: string -> string option
    abstract getAll: string -> string []
    abstract has: string -> bool
    abstract keys: unit -> string []
    abstract set: name:string * value:string -> unit
    abstract sort: unit -> unit
    abstract toString: unit -> string 
    abstract values: unit -> string [] 
    abstract entries: unit -> (string*string) []

type [<AllowNullLiteral>] URLType =
    [<Emit("new $0($1...)")>] 
    abstract Create: input:string * b: U2<URL, string> -> URL

type [<AllowNullLiteral>] URL =
    abstract hash: string  with get, set
    abstract host: string  with get, set
    abstract hostname: string  with get, set
    abstract href: string  with get, set
    abstract origin: string  
    abstract password: string  with get, set
    abstract pathname: string  with get, set
    abstract port: string  with get, set
    abstract protocol: string with get, set
    abstract search: string with get, set
    abstract searchParams: URLSearchParams
    abstract username: string with get, set
    abstract toString: unit -> string 
    abstract toJSON: unit -> string 

[<Import("*", "url")>]
type Static = 
    static member domainToASCII: string -> string = jsNative
    static member domainToUnicode: string -> string = jsNative
    static member fileURLToPath: U2<URL,string> -> string = jsNative
    static member pathToFileURL: string -> URL = jsNative
    static member format: URL * options : IFormatOptions -> string = jsNative
