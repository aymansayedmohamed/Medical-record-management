
app.service("MedicalHistoryApi", function ($http) {
    
    var ApiURL = document.querySelector('#HServicesURL').value;

    this.Get = function (SocialNumber) {

        return $http.get(ApiURL + 'api/MedicalHistory/Search?SocialNumber=' + SocialNumber);

    }

    this.DeliverByMail = function (SocialNumber) {

        return $http.get(ApiURL + 'api/MedicalHistory/DeliverByMail?SocialNumber=' + SocialNumber);

    }



});