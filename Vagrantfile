# Vagrant configuration for ProcureRiskAnalyzer
Vagrant.configure("2") do |config|
    # базовий образ Ubuntu
    config.vm.box = "ubuntu/focal64"
    config.vm.hostname = "procure-analyzer"
  
    # синхронізація локальної папки 
    config.vm.synced_folder ".", "/home/vagrant/app"
  
    # автоматичне налаштування середовища
    config.vm.provision "shell", inline: <<-SHELL
      echo "=== Step 1: Installing .NET 9 SDK ==="
      sudo apt-get update -y
      sudo apt-get install -y wget apt-transport-https software-properties-common
      wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
      sudo dpkg -i packages-microsoft-prod.deb
      sudo apt-get update -y
      sudo apt-get install -y dotnet-sdk-9.0
  
      echo "=== Step 2: Simulating NuGet package install from BaGet ==="
      cd /home/vagrant/app/ProcureRiskAnalyzer/bin/Release
      echo "Package ProcureRiskAnalyzer.1.0.0.nupkg installed successfully (simulated)."
  
      echo "=== Step 3: Running ProcureRiskAnalyzer ==="
      cd /home/vagrant/app/ProcureRiskAnalyzer
      dotnet build
      dotnet run
    SHELL
  end
  