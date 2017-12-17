open System
open FSharp.Data
open FSharp.Linq
open NUnit.Framework
open CatOwnerSorter


[<Literal>]
let testData = """[{"name": "Jack","gender": "Male","age": 20,"pets": [{"name": "Garfield","type": "Cat"},{"name": "Fido","type": "Dog"}]},{"name": "Jessica","gender": "Female","age": 21,"pets": [{"name": "Grumpy","type": "Cat"}]},{"name": "Joe","gender": "Not Specified","age": 22,"pets": [{"name": "Happy","type": "Cat"}]}]"""

[<Test>]
let ``My test`` () =
    Assert.True(true)

[<Test>]
let ``Aggregate Distinct Genders`` () =
   let data = Datasource.parseData testData
   let genders = data |> BusinessLogic.getGenders
   let expected:string list = ["Male"; "Female"; "Not Specified"]
   Assert.AreEqual(genders, expected)