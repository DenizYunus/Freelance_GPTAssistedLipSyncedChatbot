# GPT Assisted, Lip Synced Chatbot

## Overview
This project is a cutting-edge chatbot leveraging GPT for text generation and audio synthesis, combined with Oculus Lip Sync technology and custom Blender-made visemes for realistic lip-synced animations. It integrates Google Cloud Speech Recognition for accurate voice input processing, creating a seamless and engaging user experience.

## Features
- **GPT Speech Synthesis**: Utilizes GPT's capabilities to synthesize natural-sounding audio responses.
- **Oculus Lip Sync Integration**: Implements Oculus Lip Sync with custom Blender-made visemes for realistic animations.
- **GPT API for Text Generation**: Generates intelligent and contextually relevant text responses using the GPT API.
- **Google Cloud Speech Recognition**: Uses Google Cloud's speech recognition service for accurate voice command interpretation.
- **Unity Engine**: Built on Unity, this project combines high-quality visuals with dynamic interactions.

## Getting Started

### Prerequisites
- Unity 2020.3 LTS or later
- Google Cloud Account (for Speech Recognition)
- OpenAI API key (for GPT API)

### Installation
1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourrepository/GPTLipSyncedChatbot.git
   ```

2. **Open the project in Unity:**
- Launch Unity Hub.
- Add the cloned project directory.
- Select the project to open it in the Unity Editor.
3. **Set up GPT API:**
- Obtain an API key from OpenAI.
- Place your API key in `Assets/Scripts/OpenAIPromptHelper.cs`.
4. **Configure Google Cloud Speech Recognition:**
- Set up a Google Cloud project and enable the Speech-to-Text API.
- Download your credentials JSON file and place it in the `StreamingAssets` folder.
- Update the script settings with your Google Cloud project ID.

### Running the Chatbot
- Open the `MainScene` in Unity.
- Press the Play button to start the chatbot.
- Interact with the chatbot using voice commands; the chatbot will respond with synthesized speech and corresponding lip-sync animations.

## Customization
- **Visemes Customization**: Modify the Blender-made visemes to fit your model's specific needs for more accurate lip-syncing.
- **Speech Synthesis Settings**: Adjust the speech synthesis parameters to alter the voice and speaking style of the chatbot.

## Contributing
Contributions are welcome! Please read our contributing guidelines for details on how to submit pull requests.

## License
This project is licensed under the MIT License - see the LICENSE.md file for details.
