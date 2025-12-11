{
  description = "Nix flake for backend_welcome_message (.NET console app)";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs {
          inherit system;
          config = { allowUnfree = true; };
        };

        # Prefer newer SDKs when available; fall back to 8 if the channel lacks them
        dotnet =
          if pkgs ? dotnet-sdk_10 then pkgs.dotnet-sdk_10
          else if pkgs ? dotnet-sdk_9 then pkgs.dotnet-sdk_9
          else pkgs.dotnet-sdk_8;
      in
      {
        devShells.default = pkgs.mkShell {
          packages = [
            dotnet
            pkgs.git
          ];

          # Make dotnet CLI happy inside Nix
          DOTNET_ROOT = "${dotnet}";
          DOTNET_CLI_TELEMETRY_OPTOUT = "1";
        };

        # Expose a runnable package and wire it to `nix run`
        packages.default = pkgs.writeShellApplication {
          name = "run-backend-welcome-message";
          runtimeInputs = [ dotnet ];
          text = ''
            set -euo pipefail
            cache_root="''${XDG_CACHE_HOME:-$HOME/.cache}/backend-welcome-message"
            mkdir -p "$cache_root/obj" "$cache_root/bin"

            dotnet run --project "${self}/backend_welcome_message.csproj" \
              --property:BaseIntermediateOutputPath="$cache_root/obj/" \
              --property:BaseOutputPath="$cache_root/bin/" \
              "$@"
          '';
        };

        apps.default = flake-utils.lib.mkApp {
          drv = self.packages.${system}.default;
        };
      });
}
