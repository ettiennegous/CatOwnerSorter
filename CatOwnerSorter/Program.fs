open System
open FSharp.Data
open FSharp.Linq

[<Literal>]
let apiUrl = "http://agl-developer-test.azurewebsites.net/people.json"
[<Literal>]
let petTypesToFind = "Cat"
type OwnersAndCats = JsonProvider<apiUrl> //Can get inferred type from this command but it makes DEV very slow as its keeps fetching and dynamically typing

[<EntryPoint>]
let main argv =
    let responseData = OwnersAndCats.Load(apiUrl)
    //Group owners collection by gender and get unique genders
    let genders = responseData
                   |> Seq.groupBy (fun x -> x.Gender)
                   |> Seq.map (fun (key, values) -> key)
                   |> List.ofSeq

    for gender in genders do
       printfn "%s" gender
       let petsForGender = responseData
                              |> Seq.filter(fun owner -> owner.Gender = gender) //Reduce original collection for each gender, 
                              |> Seq.collect(fun owner -> owner.Pets //aggregate the many results with collect, Its like SelectMany in Linq
                                                          |> Seq.filter(fun pet -> pet.Type = petTypesToFind) //filter them by cats, 
                                                          |> Seq.map(fun pet -> pet.Name)) //Get the name of each cat
                              |> Seq.sort
       for cat in petsForGender do
          Console.WriteLine(cat)
    let pause = Console.ReadLine()
    0 // return an integer exit code
