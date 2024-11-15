from playwright.sync_api import sync_playwright
import time

def main():
    with sync_playwright() as p:
        # Inicia o navegador
        browser = p.firefox.launch(headless=False)
        page = browser.new_page()
        page.goto("https://tivo.bet/br/sportsbook/virtual")
        
        # Acesse o primeiro iframe e clica no item desejado
        page.wait_for_selector('iframe[name="sportsbook_iframe"]', timeout=15000)
        iframe = page.frame(name="sportsbook_iframe")
        
        # Verifica se o iframe foi encontrado
        if iframe is None:
            print("Erro: iframe não encontrado!")
            browser.close()
            return

        # Aguarda o seletor dentro do iframe e tenta clicar nele
        iframe.locator("#virtual-champ-select > ul:nth-child(1) > li:nth-child(4) > span").click()
        
        # Espera o segundo iframe carregar (se necessário)
        iframe2_locator = page.frame_locator("iframe[name='virtualSportsIframe']")
        iframe2 = iframe2_locator.locator("iframe").nth(0)  # Acessa o conteúdo do segundo iframe
        
        # Espera um pouco para garantir que o conteúdo do iframe esteja disponível
        time.sleep(2)  # Você pode usar o método de espera do Playwright, como `page.wait_for_selector()`

        iframe3_locator = iframe2.frame_locator('iframe[title="Virtual Sports iFrame"]')
        iframe3 = iframe3_locator.locator("iframe").nth(0)
        iframe3.locator ('div > button.results-page-button').click()
       

        # Fecha o navegador
        browser.close()

if __name__ == "__main__":
    main()
