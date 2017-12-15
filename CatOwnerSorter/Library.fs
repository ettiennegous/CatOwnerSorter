namespace CatOwnerSorter
module Datasource = 
   open FSharp.Data

   [<Literal>]
   let apiUrl = "http://agl-developer-test.azurewebsites.net/people.json"
   [<Literal>]
   let petTypesToFind = "Cat"
   type OwnersAndCats = JsonProvider<apiUrl> //Can get inferred type from this command but it makes DEV very slow as its keeps fetching and dynamically typing

   let loadData = OwnersAndCats.Load(apiUrl)

module BusinessLogic  =
   let getGenders collection = collection //Group owners collection by gender and get unique genders
                                  |> Seq.groupBy (fun (x: Datasource.OwnersAndCats.Root) -> x.Gender)
                                  |> Seq.map (fun (key, values) -> key)
                                  |> List.ofSeq

   let filterGender collection gender = collection
                                           |> Seq.filter(fun (owner: Datasource.OwnersAndCats.Root) -> owner.Gender = gender) //Reduce original collection for each gender, 

   let sortCats catCollection = catCollection |> Seq.sort

   let outputCats catCollection = for cat in catCollection do
                                    printfn "%s" cat

   let aggregateCats (ownersByGender:seq<Datasource.OwnersAndCats.Root>) = ownersByGender
                                                                           |> Seq.collect(fun pet -> pet.Pets
                                                                                                      |> Seq.filter(fun pet -> pet.Type = Datasource.petTypesToFind)
                                                                                                      |> Seq.map(fun pet -> pet.Name))