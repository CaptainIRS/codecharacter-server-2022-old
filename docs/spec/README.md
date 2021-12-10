# Code Generation Process

From root
run ```openapi-generator-cli generate -g aspnetcore -i spec/CodeCharacter-API.yaml -c spec/generator-config.yml -o . --api-name-suffix=Api --model-name-suffix=Dto```