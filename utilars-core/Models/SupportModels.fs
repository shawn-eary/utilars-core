// ©2013 - Shawn Eary
namespace utilars_core.Models

open System.ComponentModel.DataAnnotations
open System.ComponentModel
open logicLibrary

type TicketRequest() = 
   let expectedAuth = logicLibrary.imgGenerator.getImages(0)          
   let mutable base64Digits = fst(expectedAuth)
   let mutable expected = snd(expectedAuth)
   let mutable challengeResponse = ""
   let mutable name = ""
   let mutable email = ""
   let mutable message = ""
       
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

   [<Required(ErrorMessage = "Please give your name")>]
   [<StringLength(50)>]
   [<DataType(DataType.Text)>]
   [<RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Please only use standard English letters and spaces.")>]
   member x.Name
      with get() = name and set(value) = name <- value

   [<Required(ErrorMessage = "Please provide your email address")>]
   [<StringLength(50)>]
   [<DataType(DataType.EmailAddress)>]
   // https://stackoverflow.com/questions/16712043/email-address-validation-using-asp-net-mvc-data-type-attributes
   [<EmailAddress(ErrorMessage = "Invalid Email Address")>]
   member x.Email
      with get() = email and set(value) = email <- value

   [<Required(ErrorMessage = "Please describe the anomally or need")>]
   [<StringLength(500)>]
   [<DataType(DataType.Text)>]
   // https://stackoverflow.com/questions/16712043/email-address-validation-using-asp-net-mvc-data-type-attributes
   [<RegularExpression("^[a-zA-Z,! .]+$", ErrorMessage = "Please only use letters, spaces, periods and commas.")>]
   member x.Message
      with get() = message and set(value) = message <- value

