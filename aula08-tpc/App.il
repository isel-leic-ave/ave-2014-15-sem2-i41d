.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly App
{
  .ver 0:0:0:0
}
.module App.exe

.class private auto ansi beforefieldinit A extends [mscorlib]System.Object
{
  .field private string myLabel
  
  .method public hidebysig instance void  Bar(string label) cil managed
  {
    .maxstack  8
    ldstr      "Bar : "
    ldarg.1
    call       string [mscorlib]System.String::Concat(string, string)
    call       void [mscorlib]System.Console::WriteLine(string)
    ret
  } // end of method A::Bar

  .method public hidebysig instance void  Foo(string label) cil managed
  {
    .maxstack  8
    ldstr      "Foo : "
    ldarg.1
    call       string [mscorlib]System.String::Concat(string, string)
    call       void [mscorlib]System.Console::WriteLine(string)
    ldarg.0
    ldarg.1
    stfld      string A::myLabel
    ret
  } // end of method A::Foo

  .method public hidebysig specialname rtspecialname  instance void  .ctor() cil managed
  {
    .maxstack  8
    ldarg.0
    call       instance void [mscorlib]System.Object::.ctor()
    ret
  } // end of method A::.ctor

} // end of class A

.class private auto ansi beforefieldinit App extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main() cil managed
  {
    .entrypoint
    .maxstack  2
    .locals init (class A V_0)
    ldnull
    stloc.0
    ldloc.0
    ldstr      "ola"
    call   instance void A::Bar(string)
    ldloc.0
    ldstr      "super"
    call   instance void A::Foo(string)
    ret
  } // end of method App::Main

} // end of class App
