// ©2013 - Shawn Eary
namespace logicLibrary

open System
open System.Security.Cryptography

(* References 
   http://en.wikibooks.org/wiki/F_Sharp_Programming/Arrays 
   http://msdn.microsoft.com/en-us/library/dd233214.aspx
   http://msdn.microsoft.com/en-us/library/system.security.cryptography.rsacryptoserviceprovider.aspx
   http://stackoverflow.com/questions/472906/net-string-to-byte-array-c
   *) 
module imgGenerator =
   let myCryptoProvider =  new RSACryptoServiceProvider()

   let digits = [|
      "<img src='data:image/png;base64," + 
         "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACx" +
         "jwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41" +
         "LjbQg61aAAACIklEQVRYR91XzUrDQBBOWyXElipaBFEQLSKI3qUnD/6A4Fv5LOJTeBdv3gRPvoEH" +
         "rxln1911drI/k5heLIQmzczsN998O7MtAKAszAfvB/a+z2+MO1KxlxW/T6z9xuIZ4/NQXclVrBN+" +
         "b8QM8d0Kfcefe0mDItX1K94heS1JPzqZ7OIWXACEEZ+ivpvAdYD5BXgXwIEJXDXAAaz1UgKdudo+" +
         "dPGMgKB8QotXqPc+KgmIJCtKWGzxk1zQevTyBYM35OcW3TNqzwVji59xe9tYaBZ4P2V+3equ6fep" +
         "d4GsqEyJGgt4mukqPA8AwDnb89msEHxlE0gxHdUAy+I4QP9OJvCRBICLEWiZngBzegkAvLHbs63v" +
         "T+MBWG8jJq542jnFANSet8YBNScHCPW14mzNAAah54KSMbDKhOhsQxl2ZWCTMDBsU4JgjzDzIdE/" +
         "HOO27gu+3TSI2T3U48dPMlR8x0iR3Yz4Uy9QYpw8YFN+Bji8Uu11LhGVN74BJk4XiTNGMC4/B8Tq" +
         "3WDudzzvk7Kmz4SxroS/zwiduyIGIvWX+Koe4LVazkJuyuH7bQL4MiRCWxIRIGNMWbiLbj2sMT2g" +
         "ROy0JtwHEY/p9ouh8k4+AFtsKnoLa1vS2LhGOACQUCE+F7aovy5nSHyx30QgCKOSxDgbU7ptIl3s" +
         "mpXDzYnofJcgMTUNHr0k/p1tzMKqSZymsu+8wL921AcX9j/RNhmJHpTNN6YzIQFflQFIAAAAAElF" +
         "TkSuQmCC' alt='na' />";
     "<img src='data:image/png;base64," + 
      "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACx" +
      "jwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41" +
      "LjbQg61aAAADqklEQVRYR8VXe0hTYRTfXDPLmfawlrt39273c2khWrgU1su0gh4QPUgIggwaPUiy" +
      "YqWElFgUaS8KguxhGAUFGT3pn6SyP4KoKKKSSpLoQZT0UHe3ezrf2uzu5e6G0ws/xt093/n97jnn" +
      "O+e7KlWcLwBQIxLiTPPfPSUcMLJBJVISWq/N4ESkL/KwacIHGoQuVGiJCTQCLxWbOHG9wDnP4P39" +
      "uKQgXMUiYSqi08y7fy1PqXfnsF+AmCQSFxGhIyC1ICEQXoLp4x7ARON3iZjEpQMiAJqa1cTkOk5M" +
      "biD5rZK9Yh049tsgs1K3O1YBirdrxSZxWN6knrXE2CMJvEsiRadd5Q0quHVHBZZqVXOsAhStQ1I2" +
      "k3c2FhpevNuQuBoEzvVLsN6+bK1VQelRj4D3ihzFYiQYRW224dvWUt0+ifBusPBOMLFdzyodTg1Z" +
      "tLeb2G4AyW7HqIjpsfhXtEbgxLcl6Tcli/G3RDhxsndvD7UQeLQs9ShMHf9EMvNisSJnsRgJJvdV" +
      "wVP57p2Ec2qpDxSRPDELTlkzXkG2sVPMNX4vD/StuMAiicJ9vxSxGWGW29al1224PnI1vE2xSpA8" +
      "ujGQMJIAJW28T20wfNQ0BHjxNJpx62vRfiIjKQ4KccGMNJkAeJli86RHydXXHOidWpEE0ZmxXX/u" +
      "1WLmMVjYrj9mVpwSjtzny/vmw/E3tFh8MCQSsZwE6+LikrQTgLsEZwKsiSTAW8DhRzOSJ1ERckOY" +
      "UKIFYVai3Lkvh0halcN+BgvXDXlMx+FAm6jTgY7TEGP8HC0oS4aVVWmgO68JdLhiRP1Ce9I62J4w" +
      "B6pVBS1KCH02vhf1W0PPAUEC6Png5c8k0N771wNUbb0hRFIGQckpfgREYEgPU6zN4f/kF1jBL4Le" +
      "CA9VnG5oc6rB1hEUAeoIiT9RAY6EuTiiH656M650y3yu7Sym5+kTfRksNL6m9TEzmuiEtJWrRYeE" +
      "NqhW/cZd9rFNX3LZj5DFi5QItjHXYDb3AQ4ZTkIVc4X+9xwxL2oBAYRF6OTIpYyauzuY5h+UiKKM" +
      "bYUSrh32ME1w0NDwkx7REMcQ9hrmQmFjxoFhUROHWoAOKynhdO4TYE4peQfiGqIWsWITeyerX4jk" +
      "7TQgAnlI5KChROiDuqKsMGMWQrsTwqrEAXx1qSGzvX8/teiIRQQds8NtFcVbSMkbyZqDfBbQrujZ" +
      "enEhiyQMScciRkayC9Wig2ok2o9Vbzr0vggoEUFtveuiro2/Oz4zs7hJ1T0AAAAASUVORK5CYII=' alt='na' />";
     "<img src='data:image/png;base64," + 
      "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACx" + 
      "jwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41" + 
      "LjbQg61aAAAC0klEQVRYR71Xv2/TQBT+QlsR0Q5FQoihqEJQmCo5tmMQEkRC3ZDY+BtYEGJhwI6T" + 
      "lP+ADYFY2GBkYenG0AGxlS0DEiwFJCQG1Jakxzvbhy/Onf1MKJZO/pF37/veu/crwBFfQogGrWNH" + 
      "DJOrl4D/BeyWK4RaNkCBe0IuFiGOQl0RR14RYJHgKGRZkgklZ2+x3npM9MMcrSUT0IVzohGhtVqH" + 
      "hEm26thgitgQrf0+Lot0tbN7+h7B26tDqpanu7i+ngMHGrB8zt9jtA+4JNgEYmx0cnBlvbwHhzGc" + 
      "UYzWsEfPSqZIoMrVlelKiv8olyBdeN/76DR1oAj+1t8SqPSYUiyttAn34b5O5YKpXJceqAQpEyDF" + 
      "L1LLr16yyUUIfto8MBO43EzR7pBl9+3Wd5oKPIQ/nhmwroIY/ntF4BE21vT9lQFmAqtVt0lBiPZY" + 
      "nX8RcOo96wmqMho7JJdAD94dPUNCuF+q2q3eEzJw2aLzrsl1WR9X7iqrM/dbM8TaIW3tWidRJERW" + 
      "v8yB0woYIwhtxUdPQaUrs/wE3ReM5OiH+SLwAMGD3N2q9AZiAPdZWaPRq2CZYRM6SLApSciPctMA" + 
      "3vPJUhwcbuLaTX3T+tndxsXVvYaz8nliCuIeaTEOlunDKQkQwf0xCe68qpumZfLK0KIHliSBh/C+" + 
      "6R3w8fnbnlh4az43JqtihhHOcaOXqNksa+BJhIvhwcxDJns0k2dsasHUkPZ7cHeo7z+J4W3T8zuK" + 
      "kU36vkUZ8oFa8yf6Ph7Af1NW5FizYRfOR/MQknY9fRAxkbXmf50JWVdCgDdoPaXav9uF/0sOJDI1" + 
      "e/BHectujdLhJNhhhkW5mB4gchhV0vqz+iYwnDlGkrznMhdfR8m4zTpPtlIhFk2RaiP2zwkk6Tbd" + 
      "MudUVSwawk4rrgemAIQ4TdafrBPRqs0adLGPN9lLihZpnaGVeIBzSdlsX+2/4b8BGKpoAltMN6wA" + 
      "AAAASUVORK5CYII=' alt='na' />";
     "<img src='data:image/png;base64," + 
      "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACx" +
      "jwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41" +
      "LjbQg61aAAAClElEQVRYR9WVP2gUQRSH9/LPEwUttggaIYIIkiIQLDQREVS4IgjBTsTKJqAS0MLC" +
      "IiAoaJXGRsRaRRRRWwULEQRBBBvBSiwiUXJXKCQZv7eZOebmZndmzSbBg4+5nZmd32/evHmbJIFf" +
      "I1HnQLmE3qtkHNHdsBBjwDcn1Bc0yQKP8hZxXw6JxWyiY01emC5adF0NILwffm+mgRehkK5bBBA+" +
      "FBKX8SoMKKVq3kREYBzebdoRGFcYuJFjYrmKCASvoUzAwAn45Bi5uGEGtIkaBua0iSdRzv+rSTFZ" +
      "78t8d5MqbdZhCtJSAajCAKKT8BMUNOGqmDg1purgv25Wtnd97crWbgTfTioKxKqBjJW0+eXbYGsG" +
      "Az2+iNDfm/WvNQKInYcWfIcPxoBl6A19DddE21gFBi4gcBRGRIRWnsVMR0R4PpZX+dZ8BDkJed0x" +
      "cW3DDBghDAxbyTldqYGRPT9qK+liA4F9edeOscs6Cu9zr+a/5gDiO2BBC9yk3a5zIaUdggNWIk6V" +
      "qg0xkxG4JeJGRLdSA4SPsKzNPc+Sc2hcZSjnWsp3OffbXOCExU/CK0+2S/Yvwh94AIdFtG0AE+1l" +
      "GdgCA+2kMS7tSYFwIHAWPtvR4P8s7M12vrrB3jwDdRn0GVDzS4UldDY52DGO4BUr4yUKd6GvcHO+" +
      "0Bunvo2z4FM4XZD1Oxm/rY/ldUwuRc9xzvolz0cKjEwwvgRf1Whr2Gw0OtdUcqkGW6HfKipZaXU+" +
      "Ovfp66oD9Jkr+BjRvmhhe0eI9wieMuvWeHnO6oD+Fozy/x78gmewLTrMuSF1rqo5Dica8/TPwEM9" +
      "fof2TCnx42NKuehrNEDb74bSyQv7iKQG7ColLpN9BqTP3OeCpOs6lhjxv2hR0RNE7d4eAAAAAElF" +
      "TkSuQmCC' alt='na' />";   
     "<img src='data:image/png;base64," + 
      "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' alt='na' />" |]
   let cirPosToASCII (x : int) = byte(x + 48)
   let getExpectedByteArray (circNums : int[]) = 
      Array.map cirPosToASCII circNums            
   let getImages (dummy:int) =    
      let seed = int32(System.DateTime.Now.Ticks)   
      let randGen = new System.Random(seed)
      let initialNums =  
         [| randGen.Next(); randGen.Next(); randGen.Next(); 
            randGen.Next(); randGen.Next() |]            
      let circularNums = Array.map (fun x -> (x % (digits.GetLength 0))) initialNums
      let returnString =
         digits.[circularNums.[0]] + 
         digits.[circularNums.[1]] + 
         digits.[circularNums.[2]] + 
         digits.[circularNums.[3]] + 
         digits.[circularNums.[4]]      
      let expectedByteArray = getExpectedByteArray circularNums      
      let encryptedByteArray = 
         myCryptoProvider.Encrypt(expectedByteArray, false)
      let encodedAndEncryptedExpectedAnswer = 
         Convert.ToBase64String(encryptedByteArray)
      let base64Decoded = 
         Convert.FromBase64String(encodedAndEncryptedExpectedAnswer)
      let decryptedByteArray = 
         myCryptoProvider.Decrypt(base64Decoded, false)
      (returnString, encodedAndEncryptedExpectedAnswer)

   (* Example: [Array.fold2]      
      Return true if the "leftArray" and "rightArray" are the same 
      length and contain the same number of elements *) 
   let areEqual (leftArray:byte[]) (rightArray:byte[]) = 
      if (leftArray.Length <> rightArray.Length) then
         false
      else
         Array.fold2 (fun accResult left right ->
            accResult && (left = right)) true leftArray rightArray
        
   (* References
      http://stackoverflow.com/questions/472906/net-string-to-byte-array-c *) 
   let responseIsValid (response:string) (expected:string) = 
      let base64Decoded = Convert.FromBase64String(expected)         
      let decryptedByte = 
         myCryptoProvider.Decrypt(base64Decoded, false)
      let expectedByte = System.Text.Encoding.UTF8.GetBytes(response)
      let returnResult = areEqual decryptedByte expectedByte
      returnResult
      
      
