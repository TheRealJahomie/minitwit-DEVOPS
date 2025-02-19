# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant.configure("2") do |config|
  config.vm.synced_folder ".", "/vagrant", disabled: true
  config.vm.provider "docker" do |docker|
    docker.image = "ubuntu:latest"
    docker.has_ssh = false   # Disable SSH since ubuntu:latest doesn't run an SSH server by default
    docker.remains_running = true
    docker.cmd = ["tail", "-f", "/dev/null"]  # Keeps the container running indefinitely
  end
end
