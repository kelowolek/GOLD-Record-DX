# GOLD Record DX 🎥

O **GOLD Record DX** é um gravador de tela ultraleve e de alta performance para Windows, desenvolvido em C# com .NET 8. Ele foi projetado para oferecer uma interface minimalista, elegante (estética Black & Gold) e total estabilidade em gravações de longa duração.

---

## 🌟 O Grande Diferencial: Resolução Dinâmica Automatizada

O **GOLD Record DX** possui um motor de captura inteligente que resolve um problema comum em softwares de gravação:

- **Adaptação de Resolução:** Se você iniciar a gravação em 1080p e alternar a resolução do monitor durante o processo, o programa ajusta o fluxo de vídeo em tempo real.
- **Sem Cortes:** O vídeo se redimensiona automaticamente para a nova tela sem gerar faixas pretas, erros de renderização ou necessidade de ajuste manual pós-gravação.

---

## ✨ Novas Funcionalidades e Melhorias

- **Seletor de Qualidade (HQ / ECO):** Agora com menu suspenso (Dropdown) para controle total do espaço em disco.
  - **HQ (High Quality):** Gravação em 6Mbps para máxima nitidez.
  - **ECO (Economy):** Gravação otimizada em 2Mbps, reduzindo drasticamente o tamanho do arquivo final sem perda severa de qualidade.
- **Seleção Dinâmica de Áudio:** Escolha entre Mixagem Estéreo ou Microfones específicos diretamente na interface.
- **Gestão de Destino:** Botão dedicado 📁 para selecionar a pasta de salvamento antes de iniciar, prevenindo erros por falta de espaço no disco principal.
- **Interface Flutuante (Topmost):** Design compacto que permanece sempre visível para controle rápido, com funções de minimizar e fechar personalizadas.

---

## 🚀 Como Usar

1. **Configuração de Áudio:** Selecione o dispositivo de entrada no primeiro menu suspenso.
2. **Escolha de Qualidade:** Selecione **HQ** para vídeos de alta definição ou **ECO** para economizar espaço em disco.
3. **Definir Pasta:** Clique no ícone da pasta para escolher o local de salvamento. (Padrão: pasta "Vídeos" do Windows).
4. **Gravar:** Clique em **GRAVAR**. O cronômetro iniciará e os seletores serão travados para sua segurança.
5. **Pausar/Parar:** Use o botão **II** para pausar a qualquer momento e **PARAR** para finalizar e gerar o arquivo `.mp4` instantaneamente.

---

## 🛠️ Tecnologias e Dependências

- **C# / WPF** (.NET 8)
- **ScreenRecorderLib:** Engine de captura de tela de baixa latência.
- **NAudio:** Gerenciamento de streams de áudio silencioso para sincronia perfeita.
- **Inno Setup:** Utilizado para gerar o instalador (.exe) único e profissional.

---

## 📸 Demonstração

> `![01](caminho/no/github/01.png)`
> > `![02](caminho/no/github/02.png)`
> > > `![03](caminho/no/github/03.png)`
> > > > `![04](caminho/no/github/04.png)`

---

## ✒️ Autor

Desenvolvido por **kelowolek**. 
Licença: Gratuito para uso pessoal e distribuição.
