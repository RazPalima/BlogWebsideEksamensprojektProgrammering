let url = "https://randomuser.me/api/"
let isAlreadyFetched = sessionStorage.getItem("fetched");
if (isAlreadyFetched == null) {
    fetch(url)
    .then(response => response.json())
    .then(data => {
        console.log("data");
        console.log(data);
        relevantInfo = {
            Name = data.results[0].name.first,
            Age = data.results[0].dob.age
        }
        localStorage.setItem("dataStore", JSON.stringify(relevantInfo));
        sessionStorage.setItem("fetched", "true");
    })
    .catch(err => console.log(err))

    const modelData = JSON.parse(localStorage.getItem("dataStore"));

    window.location.href = `"Home/Index2?Name=${modelData.name}&Age=${modelData.Age}"`
}