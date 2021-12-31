// ©2013 - Shawn Eary
namespace utilars_core.Models

open System.ComponentModel.DataAnnotations
open System.ComponentModel
open logicLibrary

type MessageAttempt() = 
   let expectedAuth = logicLibrary.imgGenerator.getImages(0)          
   let mutable base64Digits = fst(expectedAuth)
   let mutable expected = snd(expectedAuth)
   let mutable challengeResponse = ""
       
   [<DataType(DataType.Text)>]
   [<DisplayName("base64Digits")>]
   member x.Base64Digits with get() = base64Digits and set(value) = base64Digits <- value

   [<Required>]
   [<DataType(DataType.Text)>]
   [<DisplayName("expected")>]
   member x.Expected with get() = expected and set(value) = expected <- value
   
   [<Required(ErrorMessage = "Must enter numbers above")>]
   [<StringLength(10)>]
   [<DataType(DataType.Text)>]      
   [<RegularExpression("^[0-9]+$", ErrorMessage = "Only Digits Allowed.")>]
   member x.ChallengeResponse 
      with get() = challengeResponse and set(value) = challengeResponse <- value