document.addEventListener('DOMContentLoaded', () => {
  
    initForms()

})


function initForms() {
    //hitta allt som är av typen 'form':
    const forms = document.querySelectorAll('form')
    //för varje formulär jag har på sidan vill jag jag lägga till en event listener sumbit: och när vi trycker på den så preventdefault-förhindrar vi standardbeteendet
    forms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault()

            clearFormErrorMessages(form)


            const formData = new FormData(form)

            try {
                const res = await fetch(form.action, {
                    //om jag ej angivit någon metod blir det post 
                    method: 'post',
                    body: formData
                })
                if (res.ok) {
                    //om data ok vill jag stänga vår modal:
                    const modal = form.closest('.modal')
                    if (modal)
                        closeModal(modal)

                    //ladda om sidan:
                    window.location.reload()
                }
                else if (res.status === 400) {
                    //om vi ej får ok vill vi packetera upp ev felmeddelande vi får:
                    const data = await res.json()
                    if (data.errors) {
                        addFormErrorMessages(data.errors, form)
                    }

                    else if (res.status === 409) {
                        alert('Client already exists')
                    }

                    else {
                        alert('Unable to create new Client')
                    }

                }
            }
            catch {
                    console.log('catchiiing')

                }

            })
    })
}