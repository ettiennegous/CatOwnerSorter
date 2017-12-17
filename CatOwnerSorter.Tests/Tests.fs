open System
open FSharp.Data
open FSharp.Linq
open NUnit.Framework
open CatOwnerSorter


[<Literal>]
let testData = """[{"name": "Jack","gender": "Male","age": 20,"pets": [{"name": "Happy","type": "Cat"},{"name": "Fido","type": "Dog"}]},{"name": "Jessica","gender": "Female","age": 21,"pets": [{"name": "Grumpy","type": "Cat"}]},{"name": "Joe","gender": "Not Specified","age": 22,"pets": [{"name": "Garfield","type": "Cat"}]}]"""

[<Test>]
let ``My test`` () =
    Assert.True(true)

[<Test>]
let ``Aggregate Distinct Genders`` () =
   let data = Datasource.parseData testData
   let genders = data |> BusinessLogic.getGenders
   let expected:string list = ["Male"; "Female"; "Not Specified"]
   Assert.AreEqual(genders, expected)

[<Test>]
let ``Filter Gender`` () =
   let data = Datasource.parseData testData
   let filteredGenders = BusinessLogic.filterGender data "Not Specified"
   Assert.AreEqual(1, filteredGenders |> Seq.length)

[<Test>]
let ``Sort Cats`` () =
   let data = ["a"; "c"; "b"]
   let sortedExpectedData = ["a"; "b"; "c"]
   let sortedCats = BusinessLogic.sortCats data
   Assert.AreEqual(sortedExpectedData, sortedCats)
   
[<Test>]
let ``Aggregate Cats`` () =
   let data = Datasource.parseData testData
   let aggregatedExpectedData = ["Happy"; "Grumpy"; "Garfield"]
   let aggregatedCats = BusinessLogic.aggregateCats data
   Assert.AreEqual(aggregatedExpectedData, aggregatedCats)