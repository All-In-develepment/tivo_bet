from playwright.sync_api import sync_playwright
import time

def main():
    with sync_playwright() as p:
        # Inicia o navegador
        browser = p.firefox.launch(headless=False)
        page = browser.new_page()
        page.goto("https://vgpclive-vs001.akamaized.net/virtualsport/stable/dist/entryPoints/vwmfTSUOF.html?product=vwmf1&sport=vwmf&client_id=3586&language=br")
        
        # Acesse o primeiro iframe e clica no item desejado
        page.wait_for_selector('div > button.results-page-button', timeout=15000)
        page.locator('div > button.results-page-button').click()
        
        # Pega primeiro time
        total_partdas = 1
        while total_partdas <= 10:
            timeA = page.locator(f"#results-details > div.results > div > div > div > div > div:nth-child({total_partdas}) > div.home-name").inner_text()
            print("Total de partidas: ", timeA)
            total_partdas += 1
        # time.sleep(5)

        # Fecha o navegador
        browser.close()

if __name__ == "__main__":
    main()
