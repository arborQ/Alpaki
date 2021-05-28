docker build . -t arborq/react-frontend
docker push arborq/react-frontend
kubectl apply -f react-pod.yml