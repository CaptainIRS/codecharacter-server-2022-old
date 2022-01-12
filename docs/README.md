# CodeCharacter Server 2022

### Code Generation Process

From solution root run

```sh
$ openapi-generator-cli generate \
    -g aspnetcore \
    -i docs/spec/CodeCharacter-API.yaml \
    -c docs/spec/generator-config.yml \
    -o . \
    --api-name-suffix=Api \
    --model-name-suffix=Dto
```

Format the whole solution by running

```sh
$ dotnet jb cleanupcode \
    --exclude="**/*.xml;**/*.htm*;**/*.json" \
    CodeCharacter.sln
```

### API Docs:

* [Specification](spec/index.html)

### Lint/coverage related info:

* [Coverlet Coverage Summary](coverage/index.html)