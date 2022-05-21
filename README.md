# Rabbb.Functional
A functional programing helper.

---
You need add these usings
---

using Rabbb.Functional;
using static Rabbb.Functional.POIStatic;

---
methods:
---
A(): POI<T, F>;

B(POI<T, F> last_result): POI<T1, F1>;

C(POI<T1, F1> last_result): POI<T2, F2>;

D(POI<T2, F2> last_result): POI<T2, F2>;

using like:
---
A().Then(B).Then(C).Catch(D);

---
Return POI result.
---

An resolve result By return @True(resolve_result_value);

An reject result By return @False(reject_result_value);

An exception result By return @Except(exception_value);

Look POI.cs and POIStatic.cs to learn more.

---
It's an sync methods chain. Tf you need async chain, use System.Threading.Tasks.Task instead.
