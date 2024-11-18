# Use uma imagem base oficial do Python
FROM python:3.10-slim

# Define o diretório de trabalho dentro do container
WORKDIR /app

# Copia o arquivo de dependências para dentro do container
COPY requirements.txt .

# Instala as dependências do projeto
RUN pip install --no-cache-dir -r requirements.txt

# Instala os navegadores do playwright
RUN playwright install

# Copia o código da aplicação para o container
COPY . .

# Define a porta em que a aplicação será executada (opcional, mas recomendado)
EXPOSE 5000

# Define o comando de inicialização do container
CMD ["python", "tivobet.py"]
