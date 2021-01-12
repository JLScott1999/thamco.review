DOCKER_TAG=''

case "$TRAVIS_BRANCH" in
  "main")
    DOCKER_TAG=latest
    ;;
  "develop")
    DOCKER_TAG=dev
    ;;    
esac

docker build -f ./ThAmCo.Review/Dockerfile -t thamco.review:$DOCKER_TAG ./ThAmCo.Review --no-cache

docker login docker.io -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker tag thamco.review:$DOCKER_TAG docker.io/$DOCKER_USERNAME/thamco.review:$DOCKER_TAG
docker push docker.io/$DOCKER_USERNAME/thamco.review:$DOCKER_TAG

docker login https://docker.pkg.github.com -u $GITHUB_USERNAME -p $GITHUB_PASSWORD
docker tag thamco.review:$DOCKER_TAG docker.pkg.github.com/$GITHUB_USERNAME/thamco.review/thamco.review:$DOCKER_TAG
docker push docker.pkg.github.com/$GITHUB_USERNAME/thamco.review/thamco.review:$DOCKER_TAG