## About
.Net native implementation of [JSONata](http://jsonata.org) query and transformation language 

[![NuGet](https://img.shields.io/nuget/v/Jsonata.Net.Native.svg)](https://www.nuget.org/packages/Jsonata.Net.Native/)

This implementation is based on original [jsonata-js](https://github.com/jsonata-js/jsonata) source and also borrows some ideas from [go port](https://github.com/blues/jsonata-go).

## Performance

This implementation is about 100 times faster than [straightforward wrapping](https://github.com/mikhail-barg/jsonata.net.js) of original jsonata.js with Jint JS Engine for C# (the wrapping is published as [jsonata.net.js package](https://www.nuget.org/packages/Jsonata.Net.Js/).

For measurements code see [src/BenchmarkApp](https://github.com/mikhail-barg/jsonata.net.native/blob/master/src/BenchmarkApp/Program.cs) in this repo.

## [Usage](https://github.com/mikhail-barg/jsonata.net.native/blob/master/src/TestApp/Program.cs)

* simple case
```c#
using Jsonata.Net.Native;
...
JsonataQuery query = new JsonataQuery("$.a");
...
string result = query.Eval("{\"a\": \"b\"}");
Debug.Assert(result == "\"b\"");
```

* or, in case you are already working with [JSON.Net](https://www.newtonsoft.com/json) data:
```c#
JToken data = JToken.Parse("{\"a\": \"b\"}");
...
JToken result = query.Eval(data);
Debug.Assert(result.ToString(Formatting.None) == "\"b\"");
```

## C# Features

* `JsonataQuery` objects are immutable and therefore reusable and thread-safe.
* It is possible to provide additional variable bindings via `bindings` arg of `Eval()` call.
	* Additional functional bindings are work in progress (***TODO***: functionality is same as for built-in function implementations, but need to provide user API)

## JSONata language features support

The goal of the project is to implement 100% of latest JSONata version ([1.8.5](https://github.com/jsonata-js/jsonata/releases/tag/v1.8.5) at the moment of writing these words), but it's still work in progress. Here's is a list of features in accordance to [manual](https://docs.jsonata.org/):

* [x] [Simple Queries](https://docs.jsonata.org/simple) with support to arrays and sequence flattening.
* [x] [Predicate Queries](https://docs.jsonata.org/predicate) and wildcards.
* [x] [Functions and Expressions](https://docs.jsonata.org/expressions).
***TODO: list all other implemented stuff***

#### Detailed results for the reference test suite

We [use](https://github.com/mikhail-barg/jsonata.net.native/tree/master/src/Jsonata.Net.Native.TestSuite) the test suite from [original JSONata JS implementation](https://github.com/jsonata-js/jsonata/blob/master/test/test-suite/TESTSUITE.md) to check consistency and completeness of the port. 
Current test results for the latest test run are: 
* ![_all](https://img.shields.io/endpoint?style=for-the-badge&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2F_all.json)

[Full](https://github.com/mikhail-barg/jsonata.net.native/blob/master/src/Jsonata.Net.Native.TestSuite/TestReport/Jsonata.Net.Native.TestSuite.xml) and [brief](https://github.com/mikhail-barg/jsonata.net.native/blob/master/src/Jsonata.Net.Native.TestSuite/TestReport/extract.txt) test reports are also in the repo.
Below are current states of each test group in the suite:

* ![array-constructor](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Farray-constructor.json)
* ![blocks](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fblocks.json)
* ![boolean-expresssions](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fboolean-expresssions.json)
* ![closures](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fclosures.json)
* ![comments](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fcomments.json)
* ![comparison-operators](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fcomparison-operators.json)
* ![conditionals](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fconditionals.json)
* ![context](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fcontext.json)
* ![descendent-operator](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fdescendent-operator.json)
* ![encoding](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fencoding.json)
* ![errors](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ferrors.json)
* ![fields](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffields.json)
* ![flattening](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fflattening.json)
* ![function-abs](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-abs.json)
* ![function-append](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-append.json)
* ![function-applications](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-applications.json)
* ![function-assert](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-assert.json)
* ![function-average](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-average.json)
* ![function-boolean](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-boolean.json)
* ![function-ceil](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-ceil.json)
* ![function-contains](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-contains.json)
* ![function-count](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-count.json)
* ![function-decodeUrl](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-decodeUrl.json)
* ![function-decodeUrlComponent](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-decodeUrlComponent.json)
* ![function-distinct](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-distinct.json)
* ![function-each](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-each.json)
* ![function-encodeUrl](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-encodeUrl.json)
* ![function-encodeUrlComponent](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-encodeUrlComponent.json)
* ![function-error](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-error.json)
* ![function-eval](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-eval.json)
* ![function-exists](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-exists.json)
* ![function-floor](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-floor.json)
* ![function-formatBase](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-formatBase.json)
* ![function-formatInteger](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-formatInteger.json)
* ![function-formatNumber](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-formatNumber.json)
* ![function-fromMillis](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-fromMillis.json)
* ![function-join](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-join.json)
* ![function-keys](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-keys.json)
* ![function-length](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-length.json)
* ![function-lookup](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-lookup.json)
* ![function-lowercase](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-lowercase.json)
* ![function-max](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-max.json)
* ![function-merge](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-merge.json)
* ![function-number](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-number.json)
* ![function-pad](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-pad.json)
* ![function-parseInteger](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-parseInteger.json)
* ![function-power](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-power.json)
* ![function-replace](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-replace.json)
* ![function-reverse](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-reverse.json)
* ![function-round](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-round.json)
* ![function-shuffle](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-shuffle.json)
* ![function-sift](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-sift.json)
* ![function-signatures](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-signatures.json)
* ![function-sort](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-sort.json)
* ![function-split](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-split.json)
* ![function-spread](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-spread.json)
* ![function-sqrt](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-sqrt.json)
* ![function-string](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-string.json)
* ![function-substring](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-substring.json)
* ![function-substringAfter](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-substringAfter.json)
* ![function-substringBefore](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-substringBefore.json)
* ![function-sum](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-sum.json)
* ![function-tomillis](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-tomillis.json)
* ![function-trim](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-trim.json)
* ![function-typeOf](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-typeOf.json)
* ![function-uppercase](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-uppercase.json)
* ![function-zip](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ffunction-zip.json)
* ![higher-order-functions](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fhigher-order-functions.json)
* ![hof-filter](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fhof-filter.json)
* ![hof-map](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fhof-map.json)
* ![hof-reduce](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fhof-reduce.json)
* ![hof-single](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fhof-single.json)
* ![hof-zip-map](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fhof-zip-map.json)
* ![inclusion-operator](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Finclusion-operator.json)
* ![joins](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fjoins.json)
* ![lambdas](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Flambdas.json)
* ![literals](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fliterals.json)
* ![matchers](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fmatchers.json)
* ![missing-paths](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fmissing-paths.json)
* ![multiple-array-selectors](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fmultiple-array-selectors.json)
* ![null](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fnull.json)
* ![numeric-operators](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fnumeric-operators.json)
* ![object-constructor](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fobject-constructor.json)
* ![parentheses](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fparentheses.json)
* ![parent-operator](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fparent-operator.json)
* ![partial-application](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fpartial-application.json)
* ![predicates](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fpredicates.json)
* ![quoted-selectors](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fquoted-selectors.json)
* ![range-operator](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Frange-operator.json)
* ![regex](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fregex.json)
* ![simple-array-selectors](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fsimple-array-selectors.json)
* ![sorting](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fsorting.json)
* ![string-concat](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fstring-concat.json)
* ![tail-recursion](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ftail-recursion.json)
* ![token-conversion](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ftoken-conversion.json)
* ![transform](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ftransform.json)
* ![transforms](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Ftransforms.json)
* ![variables](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fvariables.json)
* ![wildcards](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fraw.githubusercontent.com%2Fmikhail-barg%2Fjsonata.net.native%2Fmaster%2Fsrc%2FJsonata.Net.Native.TestSuite%2FTestReport%2Fextract%2Fwildcards.json)
