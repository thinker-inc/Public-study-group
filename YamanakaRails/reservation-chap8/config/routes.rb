Rails.application.routes.draw do
#  get 'entries/index'
#  get 'entries/new'
#  get 'entries/confirm'
#  get 'entries/create'
#  get 'entries/destroy'
  root "top#index"
  resources :rooms

  resources :entries, only: [:new, :create, :destroy, :index], path: :rentals do
    post :confirm, on: :collection
  end
end
