from playwright.sync_api import sync_playwright
import time
def coletar_resultados(page, liga_nome):
    print(f"\n--- Coletando resultados para a Liga {liga_nome} ---")
    total_partidas = 1

    while total_partidas <= 10:
        try:
            # Time A
            time_a_selector = f"#results-details > div.results > div > div > div > div > div:nth-child({total_partidas}) > div.home-name"
            time_a = page.locator(time_a_selector).inner_text()
            
            # Time B
            time_b_selector = f"#results-details > div.results > div > div > div > div > div:nth-child({total_partidas}) > div.away-name"
            time_b = page.locator(time_b_selector).inner_text()
            
            # Resultado (1º tempo e final)
            score_selector = f"#results-details > div.results > div > div > div > div > div:nth-child({total_partidas}) > div.score-info > div"
            score_text = page.locator(score_selector).inner_text()
            resultado_final, resultado_primeiro_tempo = score_text.split("(")
            resultado_primeiro_tempo = resultado_primeiro_tempo.replace(")", "").strip()
            
            # ID da partida
            id_selector = f"#results-details > div.results > div > div > div > div > div:nth-child({total_partidas}) > div.date > div.match-id"
            match_id = page.locator(id_selector).inner_text()
            
            # Data e Hora
            datetime_selector = f"#results-details > div.results > div > div > div > div > div:nth-child({total_partidas}) > div.date > div.date-time"
            datetime = page.locator(datetime_selector).inner_text()
            
            # Imprime as informações da partida
            print(f"Partida {total_partidas}: {time_a} vs {time_b}")
            print(f"  - ID: {match_id}")
            print(f"  - Data e Hora: {datetime}")
            print(f"  - Resultado 1º Tempo: {resultado_primeiro_tempo}")
            print(f"  - Resultado Final: {resultado_final.strip()}\n")
        
        except Exception as e:
            print(f"Erro ao processar a partida {total_partidas}: {e}")
        
        total_partidas += 1


def abrir_pagina_resultados(page):
    """Clica no botão para abrir a página de resultados."""
    page.locator('div > button.results-page-button').click()
    page.wait_for_selector('#results-details > div.results', timeout=15000)  # Aguarda o carregamento da página de resultados


def main():
    with sync_playwright() as p:
        # Inicia o navegador
        browser = p.firefox.launch(headless=False)
        page = browser.new_page()
        page.goto("https://vgpclive-vs001.akamaized.net/virtualsport/stable/dist/entryPoints/vwmfTSUOF.html?product=vwmf1&sport=vwmf&client_id=3586&language=br")
        
        # Aguarda a página carregar
        abrir_pagina_resultados(page)

        # Coleta dados da Liga Inglesa (padrão inicial)
        coletar_resultados(page, "Inglesa")
        
        time.sleep(5)
        # Troca para a Liga Espanhola
        page.locator('body > gaming-vgpc-virtualsport > nav > gaming-vgpc-tournament-switcher > button:nth-child(3)').click()
        abrir_pagina_resultados(page)
        coletar_resultados(page, "Espanhola")
        time.sleep(5)

        # Troca para a Liga Turca
        page.locator('button:nth-child(4)').click()
        abrir_pagina_resultados(page)
        coletar_resultados(page, "Turca")
        
        # Fecha o navegador
        browser.close()

if __name__ == "__main__":
    main()
