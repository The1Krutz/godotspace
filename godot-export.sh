godot --export "Linux/X11" "./.export/Linux/GodotSpace.x86_64" --no-window
godot --export "Windows Desktop" "./.export/Windows/GodotSpace.exe" --no-window
godot --export "HTML5" "./.export/HTML/GodotSpace.html" --no-window

cp .export/HTML/GodotSpace.html .export/HTML/index.html -u