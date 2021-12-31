// ©2013 - Shawn Eary
namespace utilars_core.Models

type BlogEntry(title : string, HTML : string) = 
   let _title = title
   let _HTML = HTML
   member this.Title with get() = _title
   member this.HTML with get() = _HTML
