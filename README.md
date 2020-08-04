# Software-Arena


To access the unity project
1. Pull origin/Clone from this Repo
2. Go to unity hub, click 'add' and select /Github/Software-Arena/Software Arena
3. Run the project





Do following to include firebase project set up to unity project
1. login with firebase console(https://console.firebase.google.com/)
2. download 'google-services.json' from firebase project
3. create a 'StreamingAssets' folder under Assets folder
4. place 'google-services.json' under 'StreamingAssets'
5. please note that 'google-services.json' should be kept private, do not upload to any cloude storage including github

Do following to add Firebase unity SDK to unity project
1. go https://firebase.google.com/docs/unity/setup#add-sdks
2. click on 'Firebase Unity SDK' to download sdk
3. extract sdk file
4. open unity project, on ur Assets windows right and select "Import Packge" > "Custom Package"
5. Click on Import
6. add "\dotnet4\FirebaseAuth.unitypackage"
7. add "\dotnet4\FirebaseDatabase.unitypackage"

Do following to include facebook sdk to unity project
1. Go https://developers.facebook.com/docs/unity/downloads/
2. Click 7.18.1 to download sdk
3. Extract sdk file
4. Open unity project, on ur Assets windows right and select "Import Packge" > "Custom Package"
5. Click on Import
6. Add FacebookSDK/facebook-unity-sdk-7.18.1

Do following to include Cross Platform Native Plugins - Lite Version to unity project
1. Go to asset store in the unity editor
2. Search Cross Platform Native Plugins - Lite Version
3. Download and import the asset.

How to use Photon Network

1. Go to asset store in the unity editor
2. Type "PUN" in searchbox. A "PUN2 - FREE" asset will appear. 
3. Download and import the Photon Pun asset.
4. "PUN WIZARD" will pop up. Click on setup project and add the following app ID: 93b4d8d8-0190-4fd4-9407-f169ca6b33ed. This is the app ID connecting to the server.
5. If there is no pop up, click on Window->Photon Unity Networking->PUN WIZARD. Alternatively, open PUN WIZARD by pressing Alt+P
