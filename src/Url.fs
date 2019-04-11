namespace rec Node.Url

open Fable.Core
open Node.Base

type IFormatOptions = 
    abstract auth:string option 
    abstract fragment:bool option
    abstract search:bool option
    abstract unicode:bool option

type [<AllowNullLiteral>] URLSearchParams = 
    [<Emit("new $0($1...)")>] 
    abstract Create: input:U4<unit, string,obj, obj> -> URLSearchParams
    abstract append: string * string -> unit
    abstract delete: string -> unit
    abstract get: string -> string option
    abstract getAll: string -> string []
    abstract has: string -> bool
    abstract keys: unit -> string []
    abstract set: name:string * value:string -> unit
    abstract sort: unit -> unit
    abstract toString: unit -> string 
    abstract values: unit -> Iterator<string*string []>
    abstract entries: unit -> Iterator<string*string []>
    //abstract forEach: string * string -> obj

type [<AllowNullLiteral>] Url<'a> =
    [<Emit("new $0($1...)")>] 
    abstract Create: input:string * b: U2<Url<string>, string> -> Url<string>
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

//type IExports =
    abstract domainToASCII: string -> string
    abstract domainToUnicode: string -> string
    abstract fileURLToPath: U2<Url<string>,string> -> string
    abstract pathToFileURL: string -> Url<string>
    abstract format: url: Url<string> * ?options: IFormatOptions -> string
