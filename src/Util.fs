namespace rec Node.Util

open Fable.Core
open Node.Base
open Fable.Core.JsInterop

[<Erase>]
type Exports =
    [<Import("TextEncoder", "util"); EmitConstructor>]
    static member TextEncoder () : TextEncoder = jsNative

[<AllowNullLiteral>]
[<Interface>]
type TextEncoder =
    /// <summary>
    /// The encoding supported by the <c>TextEncoder</c> instance. Always set to <c>'utf-8'</c>.
    /// </summary>
    abstract member encoding: string with get
    abstract member encode: ?input: string -> JS.Uint8Array
    abstract member encodeInto: src: string * dest: JS.Uint8Array -> EncodeIntoResult
 
[<AllowNullLiteral>]
[<Interface>]
type EncodeIntoResult =
    /// <summary>
    /// The read Unicode code units of input.
    /// </summary>
    abstract member read: float with get, set
    /// <summary>
    /// The written UTF-8 bytes of output.
    /// </summary>
    abstract member written: float with get, set
