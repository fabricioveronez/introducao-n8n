
### Adicionar o repositório
```
helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
helm repo update
```

### Criar a release
```
helm upgrade --install --values values.yaml prometheus prometheus-community/prometheus
```