namespace Day04

open Microsoft.VisualStudio.TestTools.UnitTesting
open Puzzle01

[<TestClass>]
type Puzzle01Tests () =

    [<TestMethod>]
    member this.PasswordMustNotContainADecreasingDigit () =
        let password = 223450
        Assert.IsFalse(IsPasswordValid password)

    [<TestMethod>]
    member this.PasswordMustContainAtLeastOneSetOfSameAdjacentDigits () =
        let password = 123789
        Assert.IsFalse(IsPasswordValid password)

    [<TestMethod>]
    member this.ValidPasswordExample1 () =
        let password = 111123
        Assert.IsTrue(IsPasswordValid password)

    [<TestMethod>]
    member this.ValidPasswordExample2 () =
        let password = 111111
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
        Assert.AreEqual(481, validPossibilities.Length)