open System
open FSharp.Data
open FSharp.Linq

[<Literal>]
let apiUrl = "http://agl-developer-test.azurewebsites.net/people.json"
[<Literal>]
let petTypesToFind = "Cat"
type OwnersAndCats = JsonProvider<apiUrl> //Can get inferred type from this command but it makes DEV very slow as its keeps fetching and dynamically typing
//type OwnersAndCats = JsonProvider<"""[{"name": "Bob","gender": "Male","age": 23,"pets": [{"name": "Garfield","type": "Cat"},{"name": "Fido","type": "Dog"}]}]""">


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
       //Reduce original collection for each gender
       let ownersOfGender = responseData
                              |> Seq.filter(fun owner -> owner.Gender = gender)
                              |> Seq.toArray 
       let allCatsByGender:string[] = [||]
       for owner in ownersOfGender do
          //Get cats of specific owner
          let catsOfOwnerOfGender = owner.Pets
                                        |> Seq.filter(fun pet -> pet.Type = petTypesToFind)
                                        |> Seq.map(fun cat -> cat.Name)
                                        |> Seq.toArray 
          //let allCatsByGender = Array.append catsOfOwnerOfGender allCatsByGender
          for cat in catsOfOwnerOfGender do
             printfn "-%s" cat
    let pause = Console.ReadLine()
    0 // return an integer exit code
