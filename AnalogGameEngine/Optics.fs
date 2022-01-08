namespace AnalogGameEngine

// TODO: add comments
// TODO: think about moving this into a small, simple optics library
module Optics =
    type Getter<'full, 'part> = 'full -> 'part
    type Setter<'full, 'part> = 'full -> 'part -> 'full

    type Lens<'full, 'part> =
        | Lens of Getter<'full, 'part> * Setter<'full, 'part>
        // Get overload
        static member (^.)(full, Lens (get, _)) = get full

        // Set overload
        static member (^=)(Lens (_, set), value) = fun full -> set full value

        // Map overload
        static member (^%)(Lens (get, set), mapper) =
            fun full -> get full |> mapper |> set full

        // Compose overload
        static member (>->)(Lens (get1, set1), Lens (get2, set2)) =
            let set outer value =
                let inner = get1 outer
                let updatedInner = set2 inner value
                let updatedOuter = set1 outer updatedInner
                updatedOuter

            Lens(get1 >> get2, set)

    type Prism<'full, 'part> =
        | Prism of Getter<'full, 'part option> * Setter<'full, 'part>
        // Get overload
        static member (^.)(full, Prism (get, _)) = get full

        // Set overload
        static member (^=)(Prism (_, set), value) = fun full -> set full value

        // Map overload
        static member (^%)(Prism (get, set), mapper) =
            fun full ->
                match get full with
                | Some value -> mapper value |> set full
                | None -> full

        // Compose overload
        static member (>->)(Lens (get1, set1), Prism (get2, set2)) =
            let set outer value =
                let inner = get1 outer
                let updatedInner = set2 inner value
                let updatedOuter = set1 outer updatedInner
                updatedOuter

            Prism(get1 >> get2, set)

        static member (>->)(Prism (get1, set1), Lens (get2, set2)) =
            let set outer value =
                match get1 outer with
                | Some inner ->
                    let updatedInner = set2 inner value
                    let updatedOuter = set1 outer updatedInner
                    updatedOuter
                | None -> outer

            Prism(get1 >> Option.map (get2), set)

        static member (>->)(Prism (get1, set1), Prism (get2, set2)) =
            let set outer value =
                match get1 outer with
                | Some inner ->
                    let updatedInner = set2 inner value
                    let updatedOuter = set1 outer updatedInner
                    updatedOuter
                | None -> outer

            Prism(get1 >> Option.bind (get2), set)

    [<RequireQualifiedAccess>]
    module Optic =
        let inline compose optic1 optic2 = optic1 >-> optic2

        let inline get optic source = source ^. optic

        let inline set optic value = optic ^= value

        let inline map optic mapper = optic ^% mapper

        module Array =
            let index<'a> i : Prism<'a array, 'a> =
                Prism((Array.tryItem i), (fun array value -> Array.updateAt i value array))
