namespace MauMauPrototype

open System

[<AutoOpen>]
module Pattern =
    let (|PInt|_|) text =
        match Int32.TryParse(text: string) with
        | true, value when value > 0 -> Some value
        | _ -> None
