namespace Day04

open Microsoft.VisualStudio.TestTools.UnitTesting
open Puzzle02

[<TestClass>]
type Puzzle02Tests () =

    [<TestMethod>]
    member this.PasswordMustNotContainADecreasingDigit () =
        let password = 223450
        Assert.IsFalse(IsPasswordValid password)

    [<TestMethod>]
    member this.PasswordHasToContainAtLeastOneSetOfAnAdjacentDoubleNumber () =
        let password = 123444
        Assert.IsFalse(IsPasswordValid password)

    [<TestMethod>]
    member this.ValidPasswordExample1 () =
        let password = 112233
        Assert.IsTrue(IsPasswordValid password)

    [<TestMethod>]
    member this.ValidPasswordExample2 () =
        let password = 111122
        Assert.IsTrue(IsPasswordValid password)

    [<TestMethod>]
    member this.CanGeneratePossiblePasswords () =
        let min = 122222
        let max = 122233
        let expected = [|
            122222
            122223
            122224
            122225
            122226
            122227
            122228
            122229
            122230
            122231
            122232
            122233
        |]
        let actual = GeneratePossiblePasswords min max
        let areEqual = expected = actual
        Assert.IsTrue(areEqual)

    [<TestMethod>]
    member this.CanGeneratePossibleValidPasswords () =
        let min = 372037
        let max = 905157
        let validPossibilities = GeneratePossibleValidPasswords min max
        Assert.AreEqual(299, validPossibilities.Length)