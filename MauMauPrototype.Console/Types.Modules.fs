namespace MauMauPrototype

open SimpleOptics

open AnalogGameEngine

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module CardColor =
    let getAll () =
        seq {
            Clubs
            Spades
            Diamonds
            Hearts
        }

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module CardValue =
    let getAll () =
        seq {
            Seven
            Eight
            Nine
            Ten
            Jack
            Queen
            King
            Ace
        }

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Player =
    let create name : Player =
        {
            data = { name = name; hand = CardList.empty }
        }

    let getName player = player |> Optic.get PlayerOptic.name
