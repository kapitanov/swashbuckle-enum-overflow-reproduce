# swashbuckle-enum-overflow-reproduce

A minimal app to reproduce Swashbuckle enum overflow bug.

## How to run

```shell
dotnet run
```

## Expected output

```json
{
  "openapi": "3.0.1",
  "info": {
    "title": "API",
    "version": "v1"
  },
  "paths": {
    "/api/flags": {
      "get": {
        "tags": [
          "Api"
        ],
        "responses": {
          "200": {
            "description": "Success",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/Flags"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/Flags"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/Flags"
                }
              }
            }
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "Flags": {
        "enum": [
          0,
          1,
          2,
          4,
          4294967295
        ],
        "type": "integer",
        "format": "int32"
      }
    }
  }
}
```

## Actual output

```
Unhandled exception. System.OverflowException: Value was either too large or too small for an Int32.
   at System.Convert.ThrowInt32OverflowException()
   at System.UInt32.System.IConvertible.ToInt32(IFormatProvider provider)
   at System.Convert.ToInt32(Object value, IFormatProvider provider)
   at System.Enum.System.IConvertible.ToInt32(IFormatProvider provider)
   at System.Convert.ChangeType(Object value, Type conversionType, IFormatProvider provider)
   at System.Convert.ChangeType(Object value, Type conversionType)
   at Swashbuckle.AspNetCore.SwaggerGen.OpenApiAnyFactory.TryCast[T](Object value, T& typedValue)
   at Swashbuckle.AspNetCore.SwaggerGen.OpenApiAnyFactory.CreateFor(OpenApiSchema schema, Object value)
   at Swashbuckle.AspNetCore.SwaggerGen.JsonEnumHandler.<>c__DisplayClass3_0.<CreateDefinitionSchema>b__1(Object value)
   at System.Linq.Enumerable.SelectEnumerableIterator`2.ToList()
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Swashbuckle.AspNetCore.SwaggerGen.JsonEnumHandler.CreateDefinitionSchema(Type type, SchemaRepository schemaRepository)
   at Swashbuckle.AspNetCore.SwaggerGen.SchemaGeneratorBase.<>c__DisplayClass4_1.<GenerateSchema>b__0()
   at Swashbuckle.AspNetCore.SwaggerGen.SchemaRepository.GetOrAdd(Type type, String schemaId, Func`1 factoryMethod)
   at Swashbuckle.AspNetCore.SwaggerGen.SchemaGeneratorBase.CreateReferenceSchema(Type type, SchemaRepository schemaRepository, Func`1 factoryMethod)
   at Swashbuckle.AspNetCore.SwaggerGen.SchemaGeneratorBase.GenerateSchema(Type type, SchemaRepository schemaRepository)
   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.CreateResponseMediaType(ModelMetadata modelMetadata, SchemaRepository schemaRespository)
   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.<>c__DisplayClass18_0.<GenerateResponse>b__2(String contentType)
   at System.Linq.Enumerable.ToDictionary[TSource,TKey,TElement](IEnumerable`1 source, Func`2 keySelector, Func`2 elementSelector, IEqualityComparer`1 comparer)
   at System.Linq.Enumerable.ToDictionary[TSource,TKey,TElement](IEnumerable`1 source, Func`2 keySelector, Func`2 elementSelector)
   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.GenerateResponse(ApiDescription apiDescription, SchemaRepository schemaRepository, String statusCode, ApiResponseType apiResponseType)
   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.GenerateResponses(ApiDescription apiDescription, SchemaRepository schemaRepository)
   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.GenerateOperation(ApiDescription apiDescription, SchemaRepository schemaRepository)
   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.GenerateOperations(IEnumerable`1 apiDescriptions, SchemaRepository schemaRepository)
   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.GeneratePaths(IEnumerable`1 apiDescriptions, SchemaRepository schemaRepository)
   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.GetSwagger(String documentName, String host, String basePath)
   at Swashbuckle.AspNetCore.Bug.Program.Main() in D:\opensource\swashbuckle-enum-overflow-reproduce\Program.cs:line 44
```