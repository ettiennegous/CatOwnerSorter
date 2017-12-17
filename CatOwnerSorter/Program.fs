open System
open FSharp.Data
open FSharp.Linq
open CatOwnerSorter

[<Literal>]
let petTypesToFind = "Cat"

[<EntryPoint>]
let main argv =
    let data = Datasource.loadData Datasource.apiUrl
    let genders = data |> BusinessLogic.getGenders
    for gender in genders do 
       printfn "%s" gender
       let ownersByGender = BusinessLogic.filterGender data gender
       BusinessLogic.aggregateCats ownersByGender |> BusinessLogic.sortCats |> BusinessLogic.outputCats
       printfn ""
    let pause = Console.ReadLine()
    0 // return an integer exit code
